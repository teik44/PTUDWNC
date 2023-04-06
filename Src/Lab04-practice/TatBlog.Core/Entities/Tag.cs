using TatBlog.Core.Contracts;

namespace TatBlog.Core.Entities;

//biểu diễn của một từ khoá trong bài viết

public class Tag : IEntity
{
    //mã từ khoá 
    public int ID { get; set; } 
    // nội dung từ khoá
    public string Name { get; set; }    
    // tên định danh đẻ tạo url 
    public string UrlSlugs { get; set; }

    // mô tả thêm từ khoá 
    public string Description { get; set; } 

    //danh sách bài viết có chứa từ khoá
     
    public IList<Post> Posts { get; set; }  
}