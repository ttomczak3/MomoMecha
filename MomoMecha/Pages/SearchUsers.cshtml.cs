using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MomoMecha.Data;
using MomoMecha.Models;
using System.Linq.Expressions;

namespace MomoMecha.Pages
{
    public class SearchUsersModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SearchUsersModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public string SearchedUsername { get; set; }
        public List<Gundam> GundamSearchResult { get; set; }
        public List<Backlog> BacklogSearchResult { get; set; }
        public List<Wishlist> WishlistSearchResult { get; set; }

        private async Task<List<T>> GetUserItems<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            if (!string.IsNullOrEmpty(SearchedUsername))
            {
                return await _context.Set<T>()
                    .Where(predicate)
                    .ToListAsync()
                    .ConfigureAwait(false);
            }

            return [];
        }

        public async Task<IActionResult> OnGetAsync(string SearchString)
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                var user = await _userManager.FindByNameAsync(SearchString.ToUpper());

                if (user != null)
                {
                    SearchedUsername = user.UserName;

                    GundamSearchResult = await GetUserItems<Gundam>(g => g.ApplicationUser.UserName == user.UserName);
                    BacklogSearchResult = await GetUserItems<Backlog>(g => g.ApplicationUser.UserName == user.UserName);
                    WishlistSearchResult = await GetUserItems<Wishlist>(g => g.ApplicationUser.UserName == user.UserName);
                }
            }

            return Page();
        }
    }
}
