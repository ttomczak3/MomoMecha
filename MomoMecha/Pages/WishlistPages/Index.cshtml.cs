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

namespace MomoMecha.Pages.WishlistPages
{
    public class IndexModel : PageModel
    {
        private readonly MomoMecha.Data.ApplicationDbContext _context;

        public IndexModel(MomoMecha.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Wishlist> Wishlist { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Wishlist
                .Where(a => a.ApplicationUser.Id == HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (!string.IsNullOrEmpty(SearchString))
            {
                query = query.Where(s => s.Name.Contains(SearchString));
            }

            Wishlist = await query.ToListAsync();
        }
    }
}
