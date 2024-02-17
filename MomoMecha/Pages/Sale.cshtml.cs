using Microsoft.AspNetCore.Mvc.RazorPages;
using MomoMecha.Models;
using MomoMecha.Services.SaleService;

namespace MomoMecha.Pages
{
    public class SaleModel : PageModel
    {
        private readonly ISale _saleService;

        public SaleModel(ISale saleService)
        {
            _saleService = saleService;
        }

        public List<Sale> Sale { get; set; }

        public async Task OnGetAsync()
        {
            Sale = await _saleService.GetSalesAsync();
        }
    }
}
