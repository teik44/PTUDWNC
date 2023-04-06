﻿namespace TatBlog.WebApi.Models
{
    public class PostDto
    {
        // ma bv
        public int Id { get; set; }
        // tieu de bv
        public string Title { get; set; }
        //mo ta nd
        public string ShortDescription { get; set; }
        // ten dinh danh url
        public string UrlSlug { get; set; }
        // duong dan den tap tin hinh anh
        public string ImageUrl { get; set; }
        // so luot xem doc
        public int ViewCount { get; set; }
        // ngay gio dang bai
        public DateTime? ModifiedDate { get; set; }
        // chuyen muc 
        public CategoryDto Category { get; set; }
        // tac gia 
        public AuthorDto Author { get; set; }
        public IList<TagDto> Tags { get; set; }
    }
}