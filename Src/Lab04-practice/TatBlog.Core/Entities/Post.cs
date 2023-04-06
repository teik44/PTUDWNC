using TatBlog.Core.Contracts;


namespace TatBlog.Core.Entities;

    public class Post:IEntity
    { 
        //mã bài viết
        public int Id { get; set; }
        // tiêu đề bài viết
        public string Title { get; set; }
        // mô tả hay giới thiệu ngắn về nội dung
        public string ShortDescripton { get; set; }
        //nội dung chi tiết của bài viết
        public string Description { get; set; }
        // metadata
        public string Meta { get; set; }    
        //tên định danh để tạo url 
        public string UrlSlug { get; set; } 
        //đường dẫn đến tập tin hình ảnh
        public  string ImageUrl { get; set; }

        // số lượt xem, đọc bài viết
        public int ViewCount { get; set; }  

        // trạng thái bài viết
        public bool Published { get; set; } 
        
        // ngày giờ đăng bài
        public DateTime PostedDate { get; set; }

        // ngày giờ cập nhật lần cuối 

        public DateTime? ModifiedDate { get; set; }

        // mã chuyên mục
        public int CategoryID { get; set; }

        // ma tác giả của bài viế
        public int AuthorID { get; set; }

        //chuyên mục  của bài viết
        public Category    Category { get; set; }
        // tác  giả của bài viết 
        public Author Author { get; set; }
        // Danh sách các từ khoá của bài viết 
         public IList<Tag> Tags { get; set; }

    }

