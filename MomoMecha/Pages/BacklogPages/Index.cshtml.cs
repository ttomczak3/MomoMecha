using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomoMecha.Models;
using MomoMecha.Services.BacklogService;

namespace MomoMecha.Pages.BacklogPages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IBacklog _backlogService;

        public IndexModel(IBacklog backlogService)
        {
            _backlogService = backlogService;
        }

        public IList<Backlog> Backlog { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public string UserName { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Backlog = await _backlogService.GetBacklogsAsync(userId, SearchString);
            UserName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
        }
    }
}
