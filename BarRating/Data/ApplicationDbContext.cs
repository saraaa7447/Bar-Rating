using BarRating.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BarRating.Data
{
    public class ApplicationDbContext : IdentityDbContext<BarAppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Bar> Bars = default!;

        public DbSet<Review> Reviews = default!;

        public DbSet<BarRating.Models.Bar>? Bar { get; set; }

        public DbSet<BarRating.Models.Review>? Review { get; set; }
    }
}