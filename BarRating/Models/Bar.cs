using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace BarRating.Models
{
    public class Bar
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 1)]
        public string? Name { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string? Description { get; set; }

        [Required]
        [StringLength(500)]
        [DisplayName("Bar Photo")]
        [Url]
        public string? ImageUrl { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
