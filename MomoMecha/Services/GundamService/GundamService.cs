using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MomoMecha.Data;
using MomoMecha.Models;

namespace MomoMecha.Services.GundamService
{
    public class GundamService : IGundam
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly PhotoService _photoService;

        public GundamService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, PhotoService photoService)
        {
            _context = context;
            _userManager = userManager;
            _photoService = photoService;
        }

        public async Task<IList<Gundam>> GetGundamsAsync(string userId, string searchString)
        {
            var query = _context.Gundams
                .Where(a => a.ApplicationUser.Id == userId);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Name.Contains(searchString));
            }

            return await query.ToListAsync();
        }

        public async Task<List<Gundam>> GetUserGundamsAsync(string username)
        {
            return await _context.Gundams
                .Where(g => g.ApplicationUser.UserName == username)
                .ToListAsync();
        }

        public async Task AddGundamAsync(Gundam gundam, string userId, IFormFile ImageFile)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _photoService.AddPhotoAsync(ImageFile);

            gundam.ApplicationUser = user;
            gundam.ImageUrl = result.Url.ToString();

            _context.Gundams.Add(gundam);
            await _context.SaveChangesAsync();
        }

        public async Task<Gundam> GetGundamByIdAsync(int id)
        {
            return await _context.Gundams.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateGundamAsync(Gundam gundam, IFormFile ImageFile)
        {
            _context.Attach(gundam).State = EntityState.Modified;
            var result = await _photoService.AddPhotoAsync(ImageFile);
            gundam.ImageUrl = result.Url.ToString();
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGundamAsync(int id)
        {
            var gundam = await _context.Gundams.FindAsync(id);
            var imageUrl = gundam.ImageUrl;
            if (gundam != null)
            {
                _ = _photoService.DeletePhotoAsync(imageUrl);
                _context.Gundams.Remove(gundam);
                await _context.SaveChangesAsync();
            }
        }

        public bool GundamExists(int id)
        {
            return _context.Gundams.Any(e => e.Id == id);
        }
    }
}
