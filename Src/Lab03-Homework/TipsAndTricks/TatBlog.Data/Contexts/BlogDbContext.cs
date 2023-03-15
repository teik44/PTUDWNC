using System;
using Microsoft.EntityFrameworkCore;
using TatBlog.Core.Entities;
using TatBlog.Data.Mappings;

namespace TatBlog.Data.Contexts
{
    public class BlogDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Post> Posts { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-15T3HLE\MSSQLSERVER01;Initial Catalog=TatBlog;Integrated Security=True;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
        //}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(CategoryMap).Assembly);
        }

        public BlogDbContext(DbContextOptions<BlogDbContext>options):base(options) 
        {
                   
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //   modelBuilder.ApplyConfigurationsFromAssembly(
        //       typeof(CategoryMap).Assembly );
        //}
    }
}
