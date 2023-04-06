using TatBlog.Core.Contracts;


namespace TatBlog.Core.Entities;

public class Category: IEntity
{
    //mã chuyên mục 
    public int Id { get; set; }
    //tên chuyên mục, chủ đề
    public string Name { get; set; }
    // tên định danh dùng để tạo url 
    public string UrlSlug { get; set; }
    //mô tả thêm về chuyên mục 
    public  string Description { get; set; }

    //Đánh dấu chuyên mục được hiển thị trên menu
    public bool ShowOnMenu {get; set; }

    // danh sách các bài viết thuộc chuyên mục
    public IList<Post> Posts { get; set; }

}
