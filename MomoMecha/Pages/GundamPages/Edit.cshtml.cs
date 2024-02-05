using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MomoMecha.Data;
using MomoMecha.Models;
using MomoMecha.Services;

namespace MomoMecha.Pages.GundamPages
{
    public class EditModel : PageModel
    {
        private readonly MomoMecha.Data.ApplicationDbContext _context;
        private readonly PhotoService _photoService;

        public EditModel(MomoMecha.Data.ApplicationDbContext context, PhotoService photoService)
        {
            _context = context;
            _photoService = photoService;
        }

        [BindProperty]
        public Gundam Gundam { get; set; } = default!;

        [BindProperty]
        public IFormFile ImageFile { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gundam =  await _context.Gundams.FirstOrDefaultAsync(m => m.Id == id);
            if (gundam == null)
            {
                return NotFound();
            }
            Gundam = gundam;
            return Page();
        }

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

            _context.Attach(Gundam).State = EntityState.Modified;
            var result = await _photoService.AddPhotoAsync(ImageFile);

            try
            {
                Gundam.ImageUrl = result.Url.ToString();
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GundamExists(Gundam.Id))
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

        private bool GundamExists(int id)
        {
            return _context.Gundams.Any(e => e.Id == id);
        }
    }
}
