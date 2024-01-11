using Blogsite.Models;
using System.ComponentModel.DataAnnotations;

namespace Blogsite.DTO_Models
{
    public class CategoryDto
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Content { get; set; }

        public virtual ICollection<PostDto>? Posts { get; set; }
    }
}
