using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITE5331FinalProject.Models
{
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Title { get; set; }

        [Required]
        public string? Author { get; set; }

        [Required]
        public DateTime PublishTime { get; set; }

        [Required]
        public int NumViews { get; set; }

        [Required]
        public int NumLikes { get; set; }

        [Required]
        public Tag Tag { get; set; }

        [Required]
        [MaxLength(3000)]
        public string? Text { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }

    public enum Tag
    {
        Top, Popular, Recommended, Essence, Normal
    }
}
