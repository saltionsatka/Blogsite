using Blogsite.Models;
using System.ComponentModel.DataAnnotations;

namespace Blogsite.DTO_Models.RequestDtos
{
    public class RequestPostDto
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? MetaTitle { get; set; }

        public string? Slug { get; set; }

        public string? Summary { get; set; }

        public bool? Published { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public DateTime PublishedAt { get; set; }

        public string? Content { get; set; }

        public virtual ICollection<Category>? Categories { get; set; }

        public virtual ICollection<Comment>? Comments { get; set; }

        public virtual ICollection<Tag>? Tags { get; set; }
    }
}
