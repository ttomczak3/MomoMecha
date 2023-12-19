using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MomoMecha.Data;
using MomoMecha.Models;
using System.Security.Claims;

namespace MomoMecha.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FavoriteUserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public FavoriteUserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<FavoriteUser>>> Get()
        {
            var user = await _context.FavoriteUsers
                    .Where(a => a.ApplicationUser.Id == HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)
                    .ToListAsync();
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<List<FavoriteUser>>> Post(FavoriteUser favoriteUser)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var existingFavoriteUser = await _context.FavoriteUsers
                .FirstOrDefaultAsync(a => a.ApplicationUser.Id == userId && a.UserName == favoriteUser.UserName);

            if (existingFavoriteUser != null)
            {
                return Conflict("Username is already in favorites.");
            }

            var user = await _userManager.FindByIdAsync(userId);

            favoriteUser.ApplicationUser = user;
            _context.FavoriteUsers.Add(favoriteUser);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<FavoriteUser>>> Delete(int id)
        {
            var favoriteUser = await _context.FavoriteUsers.FindAsync(id);
            if (favoriteUser == null)
                return BadRequest("User not found.");

            _context.FavoriteUsers.Remove(favoriteUser);
            await _context.SaveChangesAsync();

            var user = await _context.FavoriteUsers
                .Where(a => a.ApplicationUser.Id == HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)
                .ToListAsync();

            return Ok(user);
        }
    }
}