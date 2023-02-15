using Blogsite.Models;
using System.ComponentModel.DataAnnotations;

namespace Blogsite.DTO_Models
{
    public class PostDto
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Summary { get; set; }

        public string? Content { get; set; }

        public virtual ICollection<CategoryDto>? Categories { get; set; }

        public virtual ICollection<CommentDto>? Comments { get; set; }

        public virtual ICollection<TagDto>? Tags { get; set; }
    }
}
