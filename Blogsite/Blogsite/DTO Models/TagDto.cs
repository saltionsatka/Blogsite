using Blogsite.Models;
using System.ComponentModel.DataAnnotations;

namespace Blogsite.DTO_Models
{
    public class TagDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public string? Slug { get; set; }

        public string? Content { get; set; }

        public virtual ICollection<PostDto>? Posts { get; set; }
    }
}
