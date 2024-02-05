using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MomoMecha.Data;
using MomoMecha.Models;
using MomoMecha.Services;

namespace MomoMecha.Pages.GundamPages
{
    public class DeleteModel : PageModel
    {
        private readonly MomoMecha.Data.ApplicationDbContext _context;
        private readonly PhotoService _photoService;

        public DeleteModel(MomoMecha.Data.ApplicationDbContext context, PhotoService photoService)
        {
            _context = context;
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

            var gundam = await _context.Gundams.FirstOrDefaultAsync(m => m.Id == id);

            if (gundam == null)
            {
                return NotFound();
            }
            else
            {
                Gundam = gundam;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gundam = await _context.Gundams.FindAsync(id);
            var imageUrl = gundam.ImageUrl;

            if (gundam != null)
            {
                Gundam = gundam;
                _ = _photoService.DeletePhotoAsync(imageUrl);
                _context.Gundams.Remove(Gundam);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
