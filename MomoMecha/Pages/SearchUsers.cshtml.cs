using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomoMecha.Models;
using MomoMecha.Services.BacklogService;
using MomoMecha.Services.GundamService;
using MomoMecha.Services.WishlistService;

namespace MomoMecha.Pages
{
    public class SearchUsersModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IGundam _gundamService;
        private readonly IBacklog _backlogService;
        private readonly IWishlist _wishlistService;

        public SearchUsersModel(UserManager<ApplicationUser> userManager, IGundam gundamService, IBacklog backlog, IWishlist wishlist)
        {
            _userManager = userManager;
            _gundamService = gundamService;
            _backlogService = backlog;
            _wishlistService = wishlist;
        }

        public string SearchedUsername { get; set; }
        public List<Gundam> GundamSearchResult { get; set; }
        public List<Backlog> BacklogSearchResult { get; set; }
        public List<Wishlist> WishlistSearchResult { get; set; }

        public async Task<IActionResult> OnGetAsync(string SearchString)
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                var user = await _userManager.FindByNameAsync(SearchString.ToUpper());

                if (user != null)
                {
                    SearchedUsername = user.UserName;

                    GundamSearchResult = await _gundamService.GetUserGundamsAsync(user.UserName);
                    BacklogSearchResult = await _backlogService.GetUserBacklogsAsync(user.UserName);
                    WishlistSearchResult = await _wishlistService.GetUserWishlistsAsync(user.UserName);
                }
            }

            return Page();
        }
    }
}
