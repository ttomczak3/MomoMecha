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
    public class DetailsModel : PageModel
    {
        private readonly MomoMecha.Data.ApplicationDbContext _context;

        public DetailsModel(MomoMecha.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
