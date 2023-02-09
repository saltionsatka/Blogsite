using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogsite.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(75)]
        public string? Title { get; set; }

        [MaxLength(75)]
        public string? MetaTitle{ get; set; }
        
        [MaxLength(75)]
        public string? Slug { get; set; }

        [MaxLength(255)]
        public string? Summary { get; set; }

        public bool? Published { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; }

        public DateTime PublishedAt { get; set; }

        public string? Content { get; set; }

        public virtual ICollection<Category>? Categories { get; set; }

        public virtual ICollection<Comment>? Comments { get; set;  }

        public virtual ICollection<Tag>? Tags { get; set; }

    }
}
