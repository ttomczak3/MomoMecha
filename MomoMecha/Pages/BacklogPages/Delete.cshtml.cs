using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomoMecha.Models;
using MomoMecha.Services.BacklogService;

namespace MomoMecha.Pages.BacklogPages
{
    public class DeleteModel : PageModel
    {
        private readonly IBacklog _backlogService;

        public DeleteModel(IBacklog backlogService)
        {
            _backlogService = backlogService;
        }

        [BindProperty]
        public Backlog Backlog { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var backlog = await _backlogService.GetBacklogByIdAsync(id.Value);

            if (backlog == null)
            {
                return NotFound();
            }
            else
            {
                Backlog = backlog;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _backlogService.DeleteBacklogAsync(id.Value);

            return RedirectToPage("./Index");
        }
    }
}
