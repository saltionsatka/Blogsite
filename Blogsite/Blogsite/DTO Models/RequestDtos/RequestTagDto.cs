using Blogsite.Models;
using System.ComponentModel.DataAnnotations;

namespace Blogsite.DTO_Models.RequestDtos
{
    public class RequestTagDto
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Slug { get; set; }

        public string? Content { get; set; }

        public virtual ICollection<Post>? Posts { get; set; }
    }
}
