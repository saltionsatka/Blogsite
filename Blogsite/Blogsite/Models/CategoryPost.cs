using System;

namespace Blogsite.Models
{
    public class CategoryPost
    {
        public int PostsId { get; set; }
        public virtual Post? Post { get; set; }

        public int CategoriesId { get; set; }
        public virtual Category? Category { get; set; }
    }
}