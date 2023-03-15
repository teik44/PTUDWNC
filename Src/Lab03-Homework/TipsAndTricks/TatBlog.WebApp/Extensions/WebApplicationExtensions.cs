﻿using Microsoft.EntityFrameworkCore;
using TatBlog.Data.Contexts;
using TatBlog.Data.Seeders;
using TatBlog.Services.Blogs;

namespace TatBlog.WebApp.Extensions
{
    public static class WebApplicationExtensions
    {
        // Thêm các dịch vụ được yêu cầu bởi MVC Framework
        public static WebApplicationBuilder ConfigureMvc(
            this WebApplicationBuilder buider)
        {
            buider.Services.AddControllersWithViews();
            buider.Services.AddResponseCompression();

            return buider;
        }

        // Đăng kí các dịch vụ với DI Container
        public static WebApplicationBuilder ConfigureServices(
            this WebApplicationBuilder buider)
        {
            buider.Services.AddDbContext<BlogDbContext>(options =>
                options.UseSqlServer(
                    buider.Configuration
                        .GetConnectionString("DefaultConnection")));

            buider.Services.AddScoped<IBlogRepository, BlogRepository>();
            buider.Services.AddScoped<IDataSeeder, DataSeeder>();

            return buider;
        }

        // Cấu hình HTTP Request pipeline
        public static WebApplication UseRequestPipeline(
            this WebApplication app)
        {
            // Thêm middleware để hiển thị thông báo lỗi
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Blog/Error");

                // Thêm middleware cho việc áp dụng HSTS (thêm header
                // Strict-Transport-Security vào HTTP Response).
                app.UseHsts();
            }

            // Thêm middleware để tự động nén HTTP response
            app.UseResponseCompression();

            // Thêm middleware để chuyển hướng HTTP sang HTTPS
            app.UseHttpsRedirection();

            // Thêm middleware phục vụ các yêu cầu liên quan
            // tới các tập tin nội dung tĩnh như hình ảnh, css ...
            app.UseStaticFiles();

            //Thêm middleware lựa chọn endpoint phù hợp nhất
            // để xử lý một HTTP request.
            app.UseRouting();

            return app;
        }

        // Thêm dữ liệu mẫu vào CSDL
        public static IApplicationBuilder UseDataSeeder(
                this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            try
            {
                scope.ServiceProvider
                    .GetRequiredService<IDataSeeder>()
                    .Initialize();
            }
            catch (Exception ex)
            {
                scope.ServiceProvider
                    .GetRequiredService<ILogger<Program>>()
                    .LogError(ex, "Could not insert data into database");
            }
            return app;
        }
    }
}