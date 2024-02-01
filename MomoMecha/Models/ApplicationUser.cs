using Microsoft.AspNetCore.Identity;

namespace MomoMecha.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Backlog> Backlogs { get; set; }
    }
}
