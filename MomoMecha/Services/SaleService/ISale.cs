using MomoMecha.Models;

namespace MomoMecha.Services.SaleService
{
    public interface ISale
    {
        Task<List<Sale>> GetSalesAsync();
    }
}
