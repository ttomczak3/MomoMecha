using MomoMecha.Models;

namespace MomoMecha.Services.GundamService
{
    public interface IGundam
    {
        Task<IList<Gundam>> GetGundamsAsync(string userId, string searchString);
        Task<List<Gundam>> GetUserGundamsAsync(string username);
        Task AddGundamAsync(Gundam gundam, string userId, IFormFile ImageFile);
        Task<Gundam> GetGundamByIdAsync(int id);
        Task UpdateGundamAsync(Gundam gundam, IFormFile ImageFile);
        Task DeleteGundamAsync(int id);
        bool GundamExists(int id);
    }
}
