using Microsoft.EntityFrameworkCore;
using TatBlog.Core.Contracts;
using TatBlog.Core.Entities;
using TatBlog.Core.DTO;
using TatBlog.Data.Contexts;
using TatBlog.Services.Extensions;
using TatBlog.Core.Constants;
using Microsoft.Extensions.Caching.Memory;

namespace TatBlog.Services.Blogs;

public class BlogRepository : IBlogRepository
{
    private readonly BlogDbContext _context;
    private readonly IMemoryCache _memoryCache;
    public BlogRepository(BlogDbContext context, IMemoryCache memoryCache)
    {
        _context = context;
        _memoryCache = memoryCache;
    }
    //tim bai viet có tên định danh là slug
    // và được đăng vào tháng 'month' năm 'year'
    public async Task<Post> GetPostAsync(int year, int month, string slug, CancellationToken cancellationToken = default)
    {
        IQueryable<Post> postsQuery = _context.Set<Post>()
            .Include(x => x.Category)
            .Include(x => x.Author);

        if (year > 0)
        {
            postsQuery = postsQuery.Where(x => x.PostedDate.Year == year);
        }

        if (month > 0)
        {
            postsQuery = postsQuery.Where(x => x.PostedDate.Month == month);
        }
        if (!string.IsNullOrWhiteSpace(slug))
        {
            postsQuery = postsQuery.Where(x => x.UrlSlug == slug);
        }
        // bất đồng bộ sử dụng await để tách hai hàm ra với nhau dể sử lí hơn 
        return await postsQuery.FirstOrDefaultAsync(cancellationToken);

    }

    // Tìm Top N bài viết phổ được nhiều người xem nhất
    public async Task<IList<Post>> GetPopularArticlesAsync(
        int numPosts, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Post>()
            .Include(x => x.Author)
            .Include(x => x.Category)
            .OrderByDescending(p => p.ViewCount)
            .Take(numPosts)
            .ToListAsync(cancellationToken);
    }
    //kiem tra xem ten dinh danh cua bai viet da co hay chua
    public async Task<bool> IsPostSlugExistedAsync(int postID, string slug, CancellationToken cancellationToken = default)
    {

        return await _context.Set<Post>()
           .AnyAsync(x => x.Id != postID && x.UrlSlug == slug, cancellationToken);
        //throw new NotImplementedException();
    }


    // Tăng số lượt xem của một bài viết
    public async Task IncreaseViewCountAsync(
        int postId,
        CancellationToken cancellationToken = default)
    {
        await _context.Set<Post>()
            .Where(x => x.Id == postId)
            .ExecuteUpdateAsync(p =>
                    p.SetProperty(x => x.ViewCount, x => x.ViewCount + 1),
                cancellationToken);
    }


    // lay danh sach chuyen muc va so luong 
    //tung chuyen muc chu de
  
    public async Task<IList<CategoryItem>> GetCategoriesAsync(
        bool showOnMenu = false, CancellationToken cancellationToken = default)
    {
        IQueryable<Category> categories = _context.Set<Category>();
        if (showOnMenu)
        {
            categories = categories.Where(x => x.ShowOnMenu == showOnMenu); 
        }
        return await categories
            .OrderBy(x => x.Id)
            .Select(x => new CategoryItem()
            {
                Id = x.Id,
                Name = x.Name,
                UrlSlug = x.UrlSlug,
                Description = x.Description,
                ShowOnMenu = x.ShowOnMenu,
                PostCount = x.Posts.Count(p => p.Published)
            })
            .ToListAsync(cancellationToken);

    }

    // lay danh sach tu khoa.the phan trang theo pagingparamas
    public async Task<IPagedList<TagItem>> GetPagedTagsAsync(
           IPagingParams pagingParams,
           CancellationToken cancellationToken = default)
    {
        var tagQuery = _context.Set<Tag>()
            .Select(x => new TagItem()
            {
                ID = x.ID,
                Name = x.Name,
                UrlSlug = x.UrlSlugs,
                Description = x.Description,
                PostCount = x.Posts.Count(p => p.Published)
            });
        return await tagQuery
            .ToPagedListAsync(pagingParams, cancellationToken);  
    }
    // tìm một thẻ tag có định danh là slug

    public async Task<Tag> GetTagAsync(
        string slug, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Tag>()
            .Where(x => x.UrlSlugs == slug)
            .FirstOrDefaultAsync(cancellationToken);
    }
    //
    public async Task<IList<AuthorItem>> GetAuthorsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<Author>()
        .OrderBy(a => a.FullName)
        .Select(a => new AuthorItem()
        {
            Id = a.ID,
            FullName = a.FullName,
            Email = a.ToString(),
            JoinedDate = a.JoinedDate,
            ImageUrl = a.ImageUrl,
            UrlSlug = a.UrlSlug,
            Notes = a.Notes,
            PostCount = a.Posts.Count(p => p.Published)
        })
        .ToListAsync(cancellationToken);
    }
    //1.s Tìm và phân trang 
    //đối tượng PostQuery(kết quả trả về kiểu IPagedList<Post>)
    public async Task<IPagedList<Post>> GetPagedPostsAsync(
        PostQuery condition,
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        return await FilterPosts(condition).ToPagedListAsync(
            pageNumber, pageSize,
            nameof(Post.PostedDate), "DESC",
            cancellationToken);
    }
    //
    public async Task<IPagedList<T>> GetPagedPostsAsync<T>(
        PostQuery condition,
        IPagingParams pagingParams,
        Func<IQueryable<Post>, IQueryable<T>> mapper)
    {
        var posts = FilterPosts(condition);
        var projectedPosts = mapper(posts);

        return await projectedPosts.ToPagedListAsync(pagingParams);
    }
    //
    private string GenerateSlug(string s)
    {
        return s.ToLower().Replace(".", "dot").Replace(" ", "-");
    }
    public async Task<Post> CreateOrUpdatePostAsync(Post post, IEnumerable<string> tags, CancellationToken cancellationToken = default)
    {

        if (post.Id > 0)
        {
            await _context.Entry(post).Collection(x => x.Tags).LoadAsync(cancellationToken);
        }
        else
        {
            post.Tags = new List<Tag>();
        }

        var validTags = tags.Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => new
            {
                Name = x,
                Slug = GenerateSlug(x)

            })
            .GroupBy(x => x.Slug)
            .ToDictionary(g => g.Key, g => g.First().Name);


        foreach (var kv in validTags)
        {
            if (post.Tags.Any(x => string.Compare(x.UrlSlugs, kv.Key, StringComparison.InvariantCultureIgnoreCase) == 0)) continue;

            var tag = await GetTagAsync(kv.Key, cancellationToken) ?? new Tag()
            {
                Name = kv.Value,
                Description = kv.Value,
                UrlSlugs = kv.Key
            };

            post.Tags.Add(tag);
        }

        post.Tags = post.Tags.Where(t => validTags.ContainsKey(t.UrlSlugs)).ToList();

        if (post.Id > 0)
            _context.Update(post);
        else
            _context.Add(post);

        await _context.SaveChangesAsync(cancellationToken);

        return post;
    }
    // getPostbyIDasync
    public async Task<Post> GetPostByIdAsync(
        int postId, bool includeDetails = false,
        CancellationToken cancellationToken = default)
    {
        if (!includeDetails)
        {
            return await _context.Set<Post>().FindAsync(postId);
        }

        return await _context.Set<Post>()
            .Include(x => x.Category)
            .Include(x => x.Author)
            .Include(x => x.Tags)
            .FirstOrDefaultAsync(x => x.Id == postId, cancellationToken);
    }

    private  IQueryable<Post> FilterPosts(PostQuery condition)
    {
        IQueryable<Post> posts = _context.Set<Post>()
            .Include(x => x.Category)
            .Include(x => x.Author)
            .Include(x => x.Tags);

        if (condition.PublishedOnly)
        {
            posts = posts.Where(x => x.Published);
        }

        if (condition.NotPublisded)
        {
            posts = posts.Where(x => !x.Published);
        }

        if (condition.CategoryId > 0)
        {
            posts = posts.Where(x => x.CategoryID == condition.CategoryId);
        }

        if (!string.IsNullOrWhiteSpace(condition.CategorySlug))
        {
            posts = posts.Where(x => x.Category.UrlSlug == condition.CategorySlug);
        }

        if (condition.AuthorId > 0)
        {
            posts = posts.Where(x => x.AuthorID == condition.AuthorId);
        }

        if (!string.IsNullOrWhiteSpace(condition.AuthorSlug))
        {
            posts = posts.Where(x => x.Author.UrlSlug == condition.AuthorSlug);
        }

        if (!string.IsNullOrWhiteSpace(condition.TagSlug))
        {
            posts = posts.Where(x => x.Tags.Any(t => t.UrlSlugs == condition.TagSlug));
        }

        if (!string.IsNullOrWhiteSpace(condition.Keyword))
        {
            posts = posts.Where(x => x.Title.Contains(condition.Keyword) ||
                                     x.ShortDescripton.Contains(condition.Keyword) ||
                                     x.Description.Contains(condition.Keyword) ||
                                     x.Category.Name.Contains(condition.Keyword) ||
                                     x.Tags.Any(t => t.Name.Contains(condition.Keyword)));
        }

        if (condition.PostedYear > 0)
        {
            posts = posts.Where(x => x.PostedDate.Year == condition.PostedYear);
        }

        if (condition.PostedMonth > 0)
        {
            posts = posts.Where(x => x.PostedDate.Month == condition.PostedMonth);
        }

        if (!string.IsNullOrWhiteSpace(condition.PostSlug))
        {
            posts = posts.Where(x => x.UrlSlug == condition.PostSlug);
        }
       
        

        return posts;
       

        //// Compact version
        //return _context.Set<Post>()
        //	.Include(x => x.Category)
        //	.Include(x => x.Author)
        //	.Include(x => x.Tags)
        //	.WhereIf(condition.PublishedOnly, x => x.Published)
        //	.WhereIf(condition.NotPublished, x => !x.Published)
        //	.WhereIf(condition.CategoryId > 0, x => x.CategoryId == condition.CategoryId)
        //	.WhereIf(!string.IsNullOrWhiteSpace(condition.CategorySlug), x => x.Category.UrlSlug == condition.CategorySlug)
        //	.WhereIf(condition.AuthorId > 0, x => x.AuthorId == condition.AuthorId)
        //	.WhereIf(!string.IsNullOrWhiteSpace(condition.AuthorSlug), x => x.Author.UrlSlug == condition.AuthorSlug)
        //	.WhereIf(!string.IsNullOrWhiteSpace(condition.TagSlug), x => x.Tags.Any(t => t.UrlSlug == condition.TagSlug))
        //	.WhereIf(!string.IsNullOrWhiteSpace(condition.Keyword), x => x.Title.Contains(condition.Keyword) ||
        //	                                                             x.ShortDescription.Contains(condition.Keyword) ||
        //	                                                             x.Description.Contains(condition.Keyword) ||
        //	                                                             x.Category.Name.Contains(condition.Keyword) ||
        //	                                                             x.Tags.Any(t => t.Name.Contains(condition.Keyword)))
        //	.WhereIf(condition.Year > 0, x => x.PostedDate.Year == condition.Year)
        //	.WhereIf(condition.Month > 0, x => x.PostedDate.Month == condition.Month)
        //	.WhereIf(!string.IsNullOrWhiteSpace(condition.TitleSlug), x => x.UrlSlug == condition.TitleSlug);
    }
    //
    public async Task<IPagedList<AuthorItem>> GetPagedAuthorsAsync(IPagingParams pagingParams, CancellationToken cancellationToken = default)
    {
        var tagQuery = _context.Set<Author>()
          .Select(x => new AuthorItem()
          {

          });

        return await tagQuery.ToPagedListAsync(pagingParams, cancellationToken);
    }
    // id category
    public async Task<Category> GetCategoryByIdAsync(int categoryId)
    {
        return await _context.Set<Category>().FindAsync(categoryId);
    }

    public async Task<Category> GetCachedCategoryByIdAsync(int categoryId)
    {
        return await _memoryCache.GetOrCreateAsync(
            $"author.by-id.{categoryId}",
            async (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                return await GetCategoryByIdAsync(categoryId);
            });
    }
    //
    public async Task<IPagedList<CategoryItem>> GetPagedCategoryAsync(
        IPagingParams pagingParams,
        string name = null,
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<Category>()
            .AsNoTracking()
            .WhereIf(!string.IsNullOrWhiteSpace(name),
                x => x.Name.Contains(name))
            .Select(a => new CategoryItem()
            {
                Id = a.Id,
                Name = a.Name,                
                UrlSlug = a.UrlSlug,
                Description = a.Description,
                ShowOnMenu = a.ShowOnMenu,
                PostCount = a.Posts.Count(p => p.Published)
            })
            .ToPagedListAsync(pagingParams, cancellationToken);
    }
    //
    public async Task<bool> IsCategorySlugExistedAsync(
        int categoryId,
        string slug,
        CancellationToken cancellationToken = default)
    {
        return await _context.Authors
            .AnyAsync(x => x.ID != categoryId && x.UrlSlug == slug, cancellationToken);
    }
    //
    public async Task<bool> AddOrUpdateAsync(
        Category category, CancellationToken cancellationToken = default)
    {
        if (category.Id > 0)
        {
            _context.Category.Update(category);
            _memoryCache.Remove($"category.by-id.{category.Id}");
        }
        else
        {
            _context.Category.Add(category);
        }

        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
    //
    public async Task<bool> DeleteCategoryAsync(
        int categoryId, CancellationToken cancellationToken = default)
    {
        return await _context.Category
            .Where(x => x.Id == categoryId)
            .ExecuteDeleteAsync(cancellationToken) > 0;
    }

    //public async Task<IPagedList<AuthorItem>> GetPagedAuthorsAsync(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    //{
    //    var authorsQuery = _context.Set<Author>()
    //        .Select(x => new AuthorItem()
    //        {
    //            Id = x.ID,
    //            FullName = x.FullName,
    //            UrlSlug = x.UrlSlug,
    //            Email = x.Email,
    //            Notes = x.Notes,
    //            PostCount = x.Posts.Count(p => p.Published)
    //        });
    //    return await authorsQuery.ToPagedListAsync(
    //        pageNumber, pageSize,
    //        nameof(Author.FullName), "DESC",
    //        cancellationToken);
    //}

}



