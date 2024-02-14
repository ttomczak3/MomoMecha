using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomoMecha.Models;
using MomoMecha.Services;
using MomoMecha.Services.GundamService;

namespace MomoMecha.Pages.GundamPages
{
    public class DeleteModel : PageModel
    {
        private readonly IGundam _gundamService;
        private readonly PhotoService _photoService;

        public DeleteModel(IGundam gundamService, PhotoService photoService)
        {
            _gundamService = gundamService;
            _photoService = photoService;
        }

        [BindProperty]
        public Gundam Gundam { get; set; } = default!;

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _gundamService.DeleteGundamAsync(id.Value);

            return RedirectToPage("./Index");
        }
    }
}
