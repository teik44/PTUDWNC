﻿namespace TatBlog.WebApi.Models
{
    public class PostDetail
    {
        //ma
        public int Id { get; set; }
        //tieu de
        public string Title { get; set; }
        // mo ta
        public string ShortDescription { get; set; }
        // noi dunn bai viet
        public string Description { get; set; }
        public string Meta { get; set; }
        public string UrlSlug { get; set; }
        public string ImageUrl { get; set; }
        public int ViewCount { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public CategoryDto Category { get; set; }
        public AuthorDto Author { get; set; }
        public IList<TagDto> Tags { get; set; }
    }
}