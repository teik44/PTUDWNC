using TatBlog.Core.Contracts;

namespace TatBlog.Core.Entities;

//biểu diễn tác giả của một bài viết
public class Author:IEntity
{
    // mã tác giả bài viết

    public int ID { get; set; }
    //tên tác giả
    public string FullName { get; set; }

    //tên định danh dùng để tạo URL
    public string UrlSlug { get; set; }

    // đường dẫn tới file hình ảnh
    public string ImageUrl { get; set; }

    //ngày bắt đầu
    public DateTime JoinedDate { get; set; }
    // địa chỉ mail
    public string Email { get; set; }  
    //ghi chú
    public string Notes { get; set; }
     
    //danh sách các bài viết của tác giả
    public IList<Post> Posts { get; set; }

}