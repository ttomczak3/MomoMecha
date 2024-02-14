using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MomoMecha.Models;
using MomoMecha.Services;
using MomoMecha.Services.GundamService;

namespace MomoMecha.Pages.GundamPages
{
    public class EditModel : PageModel
    {
        private readonly IGundam _gundamService;
        private readonly PhotoService _photoService;

        public EditModel(IGundam gundamService, PhotoService photoService)
        {
            _gundamService = gundamService;
            _photoService = photoService;
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

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gundam = await _gundamService.GetGundamByIdAsync(id.Value);
            if (gundam == null)
            {
                return NotFound();
            }

            Gundam = gundam;
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
                await _gundamService.UpdateGundamAsync(Gundam, ImageFile);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_gundamService.GundamExists(Gundam.Id))
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
