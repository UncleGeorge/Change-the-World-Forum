using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ITE5331FinalProject.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public int ArticleId { get; set; }
        [ForeignKey("ArticleId")]
        public virtual Article Article { get; set; }

        [Required]
        public string? Author { get; set; }

        [Required]
        public DateTime PublishTime { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Text { get; set; }

        [Required]
        public int NumLikes { get; set; }
    }
}
