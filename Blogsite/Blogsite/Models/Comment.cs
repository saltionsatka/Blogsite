using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogsite.Models {

    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Title { get; set; }

        public bool? Published { get; set;  }
    
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime PublishedAt { get; set; }

        public string? Content { get; set; }

        public int PostId { get; set; }
        public virtual Post? Post { get; set; }
    }
}