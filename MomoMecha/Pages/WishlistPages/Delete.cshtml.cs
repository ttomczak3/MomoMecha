using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomoMecha.Models;
using MomoMecha.Services.WishlistService;

namespace MomoMecha.Pages.WishlistPages
{
    public class DeleteModel : PageModel
    {
        private readonly IWishlist _wishlistService;

        public DeleteModel(IWishlist wishlistService)
        {
            _wishlistService = wishlistService;
        }

        [BindProperty]
        public Wishlist Wishlist { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wishlist = await _wishlistService.GetWishlistByIdAsync(id.Value);

            if (wishlist == null)
            {
                return NotFound();
            }
            else
            {
                Wishlist = wishlist;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _wishlistService.DeleteWishlistAsync(id.Value);

            return RedirectToPage("./Index");
        }
    }
}
