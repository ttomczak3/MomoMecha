using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MomoMecha.Data;
using MomoMecha.Models;

namespace MomoMecha.Pages.GundamPages
{
    public class IndexModel : PageModel
    {
        private readonly MomoMecha.Data.ApplicationDbContext _context;

        public IndexModel(MomoMecha.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Gundam> Gundam { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Gundam = await _context.Gundams
                .Where(a => a.ApplicationUser.Id == HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)
                .ToListAsync();
        }
    }
}