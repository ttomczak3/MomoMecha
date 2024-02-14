using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomoMecha.Models;
using MomoMecha.Services.BacklogService;

namespace MomoMecha.Pages.BacklogPages
{
    public class CreateModel : PageModel
    {
        private readonly IBacklog _backlogService;

        public CreateModel(IBacklog backlogService)
        {
            _backlogService = backlogService;
        }

        public IActionResult OnGet()
        {
            return Page();
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _backlogService.AddBacklogAsync(Backlog, userId);

            return RedirectToPage("./Index");
        }
    }
}
