using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;


namespace BarRating.Models
{
    public class Review
    {
        [Required]
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(64, MinimumLength = 1)]
        public string? Title { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string? Description { get; set; }

        [DisplayName("Author")]
        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public BarAppUser? User { get; set; }

        [ForeignKey("BarId")]
        public Bar? Bar { get; set; }
    }
}

