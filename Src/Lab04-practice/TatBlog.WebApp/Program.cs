using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using TatBlog.Data.Contexts;
using TatBlog.Data.Seeders;
using TatBlog.WebApp.Mapsters;
using TatBlog.Services.Blogs;
using TatBlog.WebApp.Extensions;
using TatBlog.WebApp.Validations;

var builder = WebApplication.CreateBuilder(args);
{
    builder
        .ConfigureMvc()
        .configureNLog()
        .ConfigureServices()
        .ConfigureMapster()
        .ConfigureFluentValidation();
}

var app = builder.Build();
{
    app.UseRequestPipeline();
    app.UseBlogRoutes();
    app.UseDataSeeder();
}

app.Run();
//var builder = WebApplication.CreateBuilder(args);
//{
//    // them cac dich vu 
//    builder.Services.AddControllersWithViews();
//    // dang ly dich vu vs  DI Container
//    builder.Services.AddDbContext<BlogDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//    builder.Services.AddScoped<IBlogRepository, BlogRepository>();
//    builder.Services.AddScoped<IDataSeeder,DataSeeder>();   
//}
//var app = builder.Build();
//{
//    //cấu hình http reques pipeline
//    // thêm middleware để hiển thị thông báo lỗi
//    if (app.Environment.IsDevelopment())
//    {
//        app.UseDeveloperExceptionPage();
//    }
//    else
//    {
//        app.UseExceptionHandler("/Blog/Error");
//        // thêm middleware cho việc áp dụng hsts( thêm header 
//        // strict-transport-security vào http response
//        app.UseHsts();
//    }
//    //thêm middlineware để chuyển hướng HTTp sang HTTPS
//    app.UseHttpsRedirection();

//    // them middlineware phuc vu cac yeu cau lien quan toi anh css ..
//    app.UseStaticFiles();
//    //them middline ware lua chon endpoint phu hop nhat de xu lyhttp request
//    app.UseRouting();
//    // dinh nghia route template

//    app.MapControllerRoute(
//        name: "posts-by-category",
//        pattern: "blog/category/{slug}",
//        defaults: new { Controller = "Blog", action = "Category" });
//    app.MapControllerRoute(
//       name: "posts-by-tag",
//       pattern: "Blog/tag/{slug}",
//       defaults: new { Controller = "Blog", action = "Tag" });

//    app.MapControllerRoute(
//       name: "single-post",
//       pattern: "blog/post/{year:int}/{month:int}/{day:int}/{slug}",
//       defaults: new { Controller = "Blog", action = "Post" });
//    app.MapControllerRoute(
//        name: "default",
//        pattern: "{controller=blog}/{action=Index}/{id?}");



//    // them du lieu mau 
//    using (var scope = app.Services.CreateScope())
//        {
//            var seeder =scope.ServiceProvider.GetRequiredService<IDataSeeder>();
//            seeder.Initialize();
//        }

//}
//app.Run();
