using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomoMecha.Models;
using MomoMecha.Services.GundamService;

namespace MomoMecha.Pages.GundamPages
{
    public class IndexModel : PageModel
    {
        private readonly IGundam _gundamService;

        public IndexModel(IGundam gundamService)
        {
            _gundamService = gundamService;
        }

        public IList<Gundam> Gundam { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public string UserName { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Gundam = await _gundamService.GetGundamsAsync(userId, SearchString);
            UserName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
        }
    }
}
