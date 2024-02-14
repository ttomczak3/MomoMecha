using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MomoMecha.Data;
using MomoMecha.Models;

namespace MomoMecha.Services.BacklogService
{
    public class BacklogService : IBacklog
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BacklogService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IList<Backlog>> GetBacklogsAsync(string userId, string searchString)
        {
            var query = _context.Backlogs
                .Where(a => a.ApplicationUser.Id == userId);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Name.Contains(searchString));
            }

            return await query.ToListAsync();
        }

        public async Task AddBacklogAsync(Backlog backlog, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            backlog.ApplicationUser = user;

            _context.Backlogs.Add(backlog);
            await _context.SaveChangesAsync();
        }

        public async Task<Backlog> GetBacklogByIdAsync(int id)
        {
            return await _context.Backlogs.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateBacklogAsync(Backlog backlog)
        {
            _context.Attach(backlog).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBacklogAsync(int id)
        {
            var backlog = await _context.Backlogs.FindAsync(id);
            if (backlog != null)
            {
                _context.Backlogs.Remove(backlog);
                await _context.SaveChangesAsync();
            }
        }

        public bool BacklogExists(int id)
        {
            return _context.Backlogs.Any(e => e.Id == id);
        }
    }
}
