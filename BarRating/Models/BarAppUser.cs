using BarRating.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace BarRating
{
    public class BarAppUser : IdentityUser
    {
        [Required]
        [StringLength(30, MinimumLength = 2)]
        [DisplayName("Username")]
        public string? ProfileName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [DisplayName("First Name")]
        public string? FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [DisplayName("Last Name")]
        public string? LastName { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
