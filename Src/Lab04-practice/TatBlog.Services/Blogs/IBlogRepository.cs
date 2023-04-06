using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using TatBlog.Core.Contracts;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;
using TatBlog.Core.Constants;

namespace TatBlog.Services.Blogs;

public interface IBlogRepository
{


    // tìm bài viết có định danh là slug 
    // được đăn vào tháng và năm 
    Task<Post> GetPostAsync(
        int year,
        int month,
        string slug,
        CancellationToken cancellationToken = default);

    // tim top N bai viet pho bien duoc nhiu nguoi xem nhat

    Task<IList<Post>> GetPopularArticlesAsync(
        int numPosts,
        CancellationToken cancellationToken = default);

    // kiểm tra xem tên dinh danh của bài viết đã có hay chưa 
    Task<bool> IsPostSlugExistedAsync(
        int postID, string slug,
        CancellationToken cancellationToken = default); 
    //tang so luot xem cua mot bai viet
    Task IncreaseViewCountAsync(
        int postId,
        CancellationToken cancellationToken = default);
    // lay danh sach chuyen muc so luong 
    Task<IList<CategoryItem>> GetCategoriesAsync(
        bool showOnMenu = false,
        CancellationToken cancellationToken = default);
    //lấy danh sách từ khóa thẻ / thẻ và phân trang theo các tham số pagingparams
    Task<IPagedList<TagItem>> GetPagedTagsAsync(
        IPagingParams pagingParams,
        CancellationToken cancellationToken = default);
    //1.a tìm một thẻ tag có định danh là slug
     
    Task<Tag> GetTagAsync(string slug, CancellationToken cancellationToken = default);

    //1.s
    Task<IPagedList<Post>> GetPagedPostsAsync(
         PostQuery condition,
         int pageNumber = 1,
         int pageSize = 10,
         CancellationToken cancellationToken = default);
    //
    Task<IPagedList<T>> GetPagedPostsAsync<T>(
        PostQuery condition,
        IPagingParams pagingParams,
        Func<IQueryable<Post>, IQueryable<T>> mapper);


    Task<IList<AuthorItem>> GetAuthorsAsync(CancellationToken cancellationToken = default);
    // GetPostByIDAsync
    Task<Post> GetPostByIdAsync(int postId, bool includeDetails = false, CancellationToken cancellationToken = default);
    
    //CreateOrUpdatePostAsync
    Task<Post> CreateOrUpdatePostAsync(Post post, IEnumerable<string> tags, CancellationToken cancellationToken = default);


    //
    Task<Category> GetCategoryByIdAsync(int categoryId);
    Task<Category> GetCachedCategoryByIdAsync(int categoryId);

    //
    Task<IPagedList<CategoryItem>> GetPagedCategoryAsync(
        IPagingParams pagingParams,
        string name = null,
        CancellationToken cancellationToken = default);
    //
    Task<bool> IsCategorySlugExistedAsync(
        int categoryId,
        string slug,
        CancellationToken cancellationToken = default);
    //
    Task<bool> AddOrUpdateAsync(
        Category category, CancellationToken cancellationToken = default);
    //Task<IPagedList<AuthorItem>> GetPagedAuthorsAsync(
    //    IPagingParams pagingParams,
    //    CancellationToken cancellationToken = default);

    //DeleteCategoryAsync
    Task<bool> DeleteCategoryAsync(
        int categoryId, CancellationToken cancellationToken = default);
    //Task<IPagedList<AuthorItem>> GetPagedAuthorsAsync(
    //     int pageNumber = 1,
    //    int pageSize = 10,
    //    CancellationToken cancellationToken = default);
}