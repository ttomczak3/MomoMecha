using Microsoft.AspNetCore.Identity;

namespace MomoMecha.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Gundam> Gundams { get; set; }
        public List<Backlog> Backlogs { get; set; }
        public List<Wishlist> Wishlist { get; set; }
    }
}
