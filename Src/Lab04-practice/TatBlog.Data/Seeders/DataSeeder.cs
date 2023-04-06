using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Data.Seeders;
using TatBlog.Data.Contexts;
using TatBlog.Core.Entities;

namespace TatBlog.Data.Seeders
{
    public class DataSeeder : IDataSeeder
    {
        private readonly BlogDbContext _dbContext;
        public DataSeeder(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Initialize()
        {
            _dbContext.Database.EnsureCreated();
            if (_dbContext.Posts.Any()) return;
            var authors = AddAuthors();
            var categories = AddCategories();
            var tags = AddTags();
            var posts = AddPosts(authors, categories, tags);

        }
        private IList<Author> AddAuthors()
        {
            var authors = new List<Author>()
            {
                new ()
                {
                    FullName = "CZ Bnb",
                    UrlSlug = "cz-bnb",
                    Email="czbnb@gmail.com",
                    JoinedDate= new DateTime (2020,10,21),
                    Notes= "Le Van Tai",
                },
                new ()
                {
                    FullName = "Elon Musk",
                    UrlSlug = "elon-musk",
                    Email="elonmusk@gmail.com",
                    JoinedDate= new DateTime (2020,4,19),
                    Notes= "hahaa",

                },
                new ()
                {
                    FullName = "Van Tai",
                    UrlSlug = "van-tai",
                    Email="vantai@gmail.com",
                    JoinedDate= new DateTime (2020,4,19),
                    Notes= "khong haha",

                }
            };
            _dbContext.Authors.AddRange(authors);
            _dbContext.SaveChanges();
            return authors;
        }


        private IList<Category> AddCategories()
        {
            var categories = new List<Category>()
            {
                new(){Name=".NET Core", Description=".NET Core", UrlSlug= ".khong co gi het", ShowOnMenu= true},
                new() {Name = "Architecture", Description = "Architecture",UrlSlug= ".Cung chang co Architec", ShowOnMenu= true },
                new() {Name = "OOP", Description = "Object-Oriented", ShowOnMenu= true },
                new() {Name = "Messaging", Description = "Messaging",UrlSlug= ".Cai Gi Z Tr", ShowOnMenu= true },


            };
            _dbContext.AddRange(categories);
            _dbContext.SaveChanges();

            return categories;
        }

        private IList<Tag> AddTags()
        {
            var tags = new List<Tag>()
            {
                new(){Name = "Metaverse", Description="Metaverse", UrlSlugs = "mtv"},
                new(){Name = "Defi", Description="Defi", UrlSlugs = "df"},
                new(){Name = "FanToken", Description="FanToken", UrlSlugs = "ftk"},
                new(){Name = "Polkadot", Description="Polkadot", UrlSlugs = "pk"},


            };
            _dbContext.AddRange(tags);
            _dbContext.SaveChanges();
            return tags;
        }

        private IList<Post> AddPosts(
             IList<Author> authors,
             IList<Category> categories,
             IList<Tag> tags)
        {
            var posts = new List<Post>()
            {
                new()
                {
                    Title="Metaverse",
                    ShortDescripton = "Metaverse (tạm dịch là vũ trụ ảo) là một vũ trụ kỹ thuật số kết hợp các khía cạnh của truyền thông xã hội, trò chơi trực tuyến, thực tế tăng cường (AR), thực tế",
                    Description ="Khi Metaverse phát triển, nó sẽ mở ra không gian trực tuyến tương tác của người dùng đa chiều hơn so với các công nghệ hiện tại",
                    Meta =" Sự kiện bùng nổ Metaverse",
                    UrlSlug="Nguồn gốc và đặc điểm của Metaverse là gì?",
                    Published= true,
                    PostedDate = new DateTime(2021,9,30,10,20,0),
                    ModifiedDate = null,
                    ViewCount = 10,
                    Author = authors[0],
                    Category= categories[0],
                    Tags = new List<Tag>()
                    {
                        tags[0]
                    }

                }
                
            };
            _dbContext.AddRange(posts);
            _dbContext.SaveChanges();
            return posts;
        }


    }
}