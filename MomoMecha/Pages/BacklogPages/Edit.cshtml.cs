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

namespace MomoMecha.Pages.BacklogPages
{
    public class EditModel : PageModel
    {
        private readonly MomoMecha.Data.ApplicationDbContext _context;

        public EditModel(MomoMecha.Data.ApplicationDbContext context)
        {
            _context = context;
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

            var backlog =  await _context.Backlogs.FirstOrDefaultAsync(m => m.Id == id);
            if (backlog == null)
            {
                return NotFound();
            }
            Backlog = backlog;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Backlog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BacklogExists(Backlog.Id))
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

        private bool BacklogExists(int id)
        {
            return _context.Backlogs.Any(e => e.Id == id);
        }
    }
}
