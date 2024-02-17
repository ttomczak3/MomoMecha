using MomoMecha.Models;

namespace MomoMecha.Services.WishlistService
{
    public interface IWishlist
    {
        Task<IList<Wishlist>> GetWishlistAsync(string userId, string searchString);
        Task<List<Wishlist>> GetUserWishlistsAsync(string username);
        Task AddWishlistAsync(Wishlist backlog, string userId);
        Task<Wishlist> GetWishlistByIdAsync(int id);
        Task UpdateWishlistAsync(Wishlist wishlist);
        Task DeleteWishlistAsync(int id);
        bool WishlistExists(int id);
    }
}
