using System.Diagnostics;
using System.Runtime.InteropServices;
using TatBlog.Core.DTO;
using TatBlog.Data.Contexts;
using TatBlog.Data.Seeders;//using TatBlog.Data.Seeders;
using TatBlog.Services.Blogs;
using TatBlog.WinApp;

// tao doi tuong db context de quan ly phien lam viec 
// voi csdl va trang thai cua cac doi tuong 
var context = new BlogDbContext();
// Tạo đối tượng BlogRepository
IBlogRepository blogRepo = new BlogRepository(context);

// tao doi tuong khoi tao du lieu 
var seeder = new DataSeeder(context);

//goi ham inititalize de nhap du lieu 
seeder.Initialize();
// doc danh sach 
var authors= context.Authors.ToList();
// xuat danh sach tac gia ra man hinh 
Console.WriteLine("{0,-4}{1,-30}{2,-30}{3,12}",
    "ID", "Full Name", "Email", "Joined Date");
foreach (var author in authors)
{
    Console.WriteLine("{0,-4}{1,-30}{2,-30}{3,12:MM/dd/yyyy}",
        author.ID, author.FullName, author.Email, author.JoinedDate);

}
// doc danh sach bai viet tu co so du lieu 
// lay kem ten tac gia va chuyen muc 
var posts = context.Posts 
    .Where(p => p.Published)
    .OrderBy(p => p.Title)
    .Select(p=> new
    {
        Id = p.Id,
        Tiltle = p.Title,
        Viewcount = p.ViewCount,
        PostedDate = p.PostedDate,
        Author = p.Author.FullName,
        Category = p.Category.Name

    })
    .ToList();
// xuat danh sach bai viet ra man hinh
foreach (var post in posts)
{

    Console.WriteLine("ID:          {0}", post.Id);
    Console.WriteLine("Title:       {0}", post.Tiltle);  
    Console.WriteLine("View:        {0}", post.Viewcount);  
    Console.WriteLine("Date:        {0:MM/dd/yyyy}", post.PostedDate);
    Console.WriteLine("Author:      {0}", post.Author);
    Console.WriteLine("Category:    {0}", post.Category);
    Console.WriteLine("".PadRight(80,'-'));
}
//
var postsP = await blogRepo.GetPopularArticlesAsync(3);

// Xuất danh sách bài viết ra màn hình
foreach (var post in postsP)
{
    Console.WriteLine("ID      : {0}", post.Id);
    Console.WriteLine("Title   : {0}", post.Title);
    Console.WriteLine("View    : {0}", post.ViewCount);
    Console.WriteLine("Date    : {0:MM/dd/yyyy}", post.PostedDate);
    Console.WriteLine("Author  : {0}", post.Author.FullName);
    Console.WriteLine("Category: {0}", post.Category.Name);
    Console.WriteLine("".PadRight(80, '-')); 
}
//lay danh sach chuyen muc 
var categories = await blogRepo.GetCategoriesAsync();
Console.WriteLine("{0,-5}{1,-50}{2,10}", "ID", "Name", "Count");
foreach (var category in categories)
{
    Console.WriteLine("{0,-5}{1,-50}{2,10}", category.Id, category.Name, category.PostCount);

}
// tao  doi tuong tham so phan  trang 
var pagingParams = new PagingParams
{
    PageNumber = 1, // lay ket qua trang so1
    PageSize = 5, // Laasy 5 mau tin
    SortColumn = "Name", // sap xep theo ten
    SortOrder = "DESC" //theo  chieu giam dan

};//
//lay dannh sach tu khoa
var tagsList = await blogRepo.GetPagedTagsAsync(pagingParams);

// xuat ra man hinh 
Console.WriteLine("{0,-5}{1,-50}{2,10}", "ID", "Name", "Count");
foreach (var item in tagsList)
{
    Console.WriteLine("{0,-5}{1,-50}{2,10}",
        item.ID,item.Name,item.PostCount);
}


// tìm một thẻ tag có định danh là slug
var tags = await blogRepo.GetTagAsync("pk");
{ 
// Xuất ra màn hình
Console.WriteLine("{0,-5}{1,-50}{2,-30}{3,-30}",
    "ID", "TagName", "Url", "Count");

Console.WriteLine("{0,-5}{1,-50}{2,-30}{3,-30}",
      tags.ID, tags.Name, tags.UrlSlugs, tags.Description);
}


