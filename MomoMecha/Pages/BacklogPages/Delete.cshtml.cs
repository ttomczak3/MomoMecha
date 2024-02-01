using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MomoMecha.Data;
using MomoMecha.Models;

namespace MomoMecha.Pages.BacklogPages
{
    public class DeleteModel : PageModel
    {
        private readonly MomoMecha.Data.ApplicationDbContext _context;

        public DeleteModel(MomoMecha.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Backlog Backlog { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var backlog = await _context.Backlogs.FirstOrDefaultAsync(m => m.Id == id);

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

            var backlog = await _context.Backlogs.FindAsync(id);
            if (backlog != null)
            {
                Backlog = backlog;
                _context.Backlogs.Remove(Backlog);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
