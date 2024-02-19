using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomoMecha.Models;
using MomoMecha.Services.WishlistService;

namespace MomoMecha.Pages.WishlistPages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IWishlist _wishlistService;

        public IndexModel(IWishlist wishlistService)
        {
            _wishlistService = wishlistService;
        }

        public IList<Wishlist> Wishlist { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public string UserName { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Wishlist = await _wishlistService.GetWishlistAsync(userId, SearchString);
            UserName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
        }
    }
}
