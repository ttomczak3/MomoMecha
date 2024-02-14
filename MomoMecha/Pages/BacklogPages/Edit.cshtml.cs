using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MomoMecha.Models;
using MomoMecha.Services.BacklogService;

namespace MomoMecha.Pages.BacklogPages
{
    public class EditModel : PageModel
    {
        private readonly IBacklog _backlogService;

        public EditModel(IBacklog backlogService)
        {
            _backlogService = backlogService;
        }

        [BindProperty]
        public Backlog Backlog { get; set; } = default!;

        public string[] SeriesItems { get; } = [
            "Universal Century",
            "Witch From Mercury",
            "IBO",
            "Build Fighters/Divers",
            "Gundam Wing",
            "Gundam Seed",
            "Gundam 00",
            "Gundam Thunderbolt",
            "G Gundam",
            "Gundam Age",
            "Gundam F91"
        ];

        public string[] GradeItems { get; } = [
            "MG",
            "RG",
            "PG",
            "HG",
            "SD",
            "RE",
            "RE/100",
            "FM",
            "EG"
        ];

        public string[] ScaleItems { get; } = [
            "1/144",
            "1/100",
            "1/60",
            "1/72",
            "1/48"
        ];

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

            Backlog = backlog;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _backlogService.UpdateBacklogAsync(Backlog);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_backlogService.BacklogExists(Backlog.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
