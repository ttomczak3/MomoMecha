using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MomoMecha.Models;
using MomoMecha.Services.WishlistService;

namespace MomoMecha.Pages.WishlistPages
{
    public class EditModel : PageModel
    {
        private readonly IWishlist _wishlistService;

        public EditModel(IWishlist wishlistService)
        {
            _wishlistService = wishlistService;
        }

        [BindProperty]
        public Wishlist Wishlist { get; set; } = default!;

        public string[] SeriesItems { get; } = [
            "Universal Century",
            "Witch From Mercury",
            "IBO",
            "Build Fighters/Divers",
            "Gundam Wing",
            "Gundam Seed",
            "Gundam 00",
            "Gundam Thunderbolt",
            "G Gundam",
            "Gundam Age",
            "Gundam F91"
        ];

        public string[] GradeItems { get; } = [
            "MG",
            "RG",
            "PG",
            "HG",
            "SD",
            "RE",
            "RE/100",
            "FM",
            "EG"
        ];

        public string[] ScaleItems { get; } = [
            "1/144",
            "1/100",
            "1/60",
            "1/72",
            "1/48"
        ];

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

            Wishlist = wishlist;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _wishlistService.UpdateWishlistAsync(Wishlist);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_wishlistService.WishlistExists(Wishlist.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
