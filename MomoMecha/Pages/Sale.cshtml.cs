using Microsoft.AspNetCore.Mvc.RazorPages;
using MomoMecha.Data;
using MomoMecha.Models;

namespace MomoMecha.Pages
{
    public class SaleModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public SaleModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Sale> Sale { get; set; }

        public void OnGet()
        {
            Sale = [.. _context.Sale];
        }
    }
}
