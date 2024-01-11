using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogsite.Models
{

    public class Tag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(75)]
        public string? Title { get; set; }

        [MaxLength(75)]
        public string? Slug { get; set; }

        public string? Content { get; set; }

        public virtual ICollection<Post>? Posts { get; set; }
    }
}