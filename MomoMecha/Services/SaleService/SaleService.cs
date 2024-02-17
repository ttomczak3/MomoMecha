using Microsoft.EntityFrameworkCore;
using MomoMecha.Data;
using MomoMecha.Models;

namespace MomoMecha.Services.SaleService
{
    public class SaleService : ISale
    {
        private readonly ApplicationDbContext _context;

        public SaleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Sale>> GetSalesAsync()
        {
            return await _context.Sale.ToListAsync();
        }
    }
}
