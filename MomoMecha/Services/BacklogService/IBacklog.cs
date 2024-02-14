using MomoMecha.Models;

namespace MomoMecha.Services.BacklogService
{
    public interface IBacklog
    {
        Task<IList<Backlog>> GetBacklogsAsync(string userId, string searchString);
        Task AddBacklogAsync(Backlog backlog, string userId);
        Task<Backlog> GetBacklogByIdAsync(int id);
        Task UpdateBacklogAsync(Backlog backlog);
        Task DeleteBacklogAsync(int id);
        bool BacklogExists(int id);
    }
}
