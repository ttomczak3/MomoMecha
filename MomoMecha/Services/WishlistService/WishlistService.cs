using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MomoMecha.Data;
using MomoMecha.Models;

namespace MomoMecha.Services.WishlistService
{
    public class WishlistService : IWishlist
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public WishlistService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IList<Wishlist>> GetWishlistAsync(string userId, string searchString)
        {
            var query = _context.Wishlist
                .Where(a => a.ApplicationUser.Id == userId);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Name.Contains(searchString));
            }

            return await query.ToListAsync();
        }

        public async Task<List<Wishlist>> GetUserWishlistsAsync(string username)
        {
            return await _context.Wishlist
                .Where(g => g.ApplicationUser.UserName == username)
                .ToListAsync();
        }

        public async Task AddWishlistAsync(Wishlist wishlist, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            wishlist.ApplicationUser = user;

            _context.Wishlist.Add(wishlist);
            await _context.SaveChangesAsync();
        }

        public async Task<Wishlist> GetWishlistByIdAsync(int id)
        {
            return await _context.Wishlist.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateWishlistAsync(Wishlist wishlist)
        {
            _context.Attach(wishlist).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWishlistAsync(int id)
        {
            var wishlist = await _context.Wishlist.FindAsync(id);
            if (wishlist != null)
            {
                _context.Wishlist.Remove(wishlist);
                await _context.SaveChangesAsync();
            }
        }

        public bool WishlistExists(int id)
        {
            return _context.Wishlist.Any(e => e.Id == id);
        }
    }
}
