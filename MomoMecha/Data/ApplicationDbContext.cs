using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MomoMecha.Models;

namespace MomoMecha.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Gundam> Gundams { get; set; }
        public DbSet<Backlog> Backlogs { get; set; }
        public DbSet<Wishlist> Wishlist { get; set; }
    }
}
