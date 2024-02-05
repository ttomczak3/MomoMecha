using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MomoMecha.Data;
using MomoMecha.Models;
using MomoMecha.Services;

namespace MomoMecha.Pages.GundamPages
{
    public class CreateModel : PageModel
    {
        private readonly MomoMecha.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly PhotoService _photoService;

        public CreateModel(MomoMecha.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager, PhotoService photoService)
        {
            _context = context;
            _userManager = userManager;
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
                return Page();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _photoService.AddPhotoAsync(ImageFile);

            Gundam.ApplicationUser = user;
            Gundam.ImageUrl = result.Url.ToString();

            _context.Gundams.Add(Gundam);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
