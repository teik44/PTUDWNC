using Microsoft.EntityFrameworkCore;
using TatBlog.Core.Entities;
using TatBlog.Data.Mappings;

namespace TatBlog.Data.Contexts;

public class BlogDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    
    public DbSet<Category> Category { get; set; }

    public DbSet<Post> Posts { get; set; }

    public DbSet<Tag> Tags { get; set; }

    public BlogDbContext(DbContextOptions<BlogDbContext> options)
        : base(options)
    {       
    }
    public BlogDbContext()
    { 
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(CategoryMap).Assembly);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //ket noi database
        optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-15T3HLE\MSSQLSERVER01;Initial Catalog=TatBlog;Integrated Security=True;TrustServerCertificate=True");

    }
    

} 