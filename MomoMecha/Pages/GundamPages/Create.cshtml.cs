using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomoMecha.Models;
using MomoMecha.Services;
using MomoMecha.Services.GundamService;

namespace MomoMecha.Pages.GundamPages
{
    public class CreateModel : PageModel
    {
        private readonly IGundam _gundamService;
        private readonly PhotoService _photoService;

        public CreateModel(IGundam gundamService, PhotoService photoService)
        {
            _gundamService = gundamService;
            _photoService = photoService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Gundam Gundam { get; set; } = default!;

        [BindProperty]
        public IFormFile ImageFile { get; set; } = default!;

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
                ModelState.AddModelError("ImageFile", "Image is required");
                return Page();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _gundamService.AddGundamAsync(Gundam, userId, ImageFile);

            return RedirectToPage("./Index");
        }
    }
}
