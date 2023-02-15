using Blogsite.Models;
using System.ComponentModel.DataAnnotations;

namespace Blogsite.DTO_Models
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public string? Content { get; set; }

        public virtual PostDto? Post { get; set; }
    }
}
