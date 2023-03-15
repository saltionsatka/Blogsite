using Blogsite.Models;
using System.ComponentModel.DataAnnotations;

namespace Blogsite.DTO_Models.RequestDtos
{
    public class RequestCommentDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public bool? Published { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime PublishedAt { get; set; }

        public string? Content { get; set; }

        public int PostId { get; set; }

        public virtual Post? Post { get; set; }
    }
}
