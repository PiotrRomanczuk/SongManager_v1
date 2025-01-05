using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SongsAPI.Data;
using SongsAPI.Models.Users;

namespace SongsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApplicationUser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetApplicationUsers()
        {
            return await _context.AppUsers.ToListAsync();
        }

        // GET: api/ApplicationUser/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUser>> GetApplicationUser(string id)
        {
            var user = await _context.AppUsers.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/ApplicationUser/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicationUser(string id, ApplicationUser user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ApplicationUser
        [HttpPost]
        public async Task<ActionResult<ApplicationUser>> PostApplicationUser(ApplicationUser user)
        {
            _context.AppUsers.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplicationUser", new { id = user.Id }, user);
        }

        // DELETE: api/ApplicationUser/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationUser(string id)
        {
            var user = await _context.AppUsers.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.AppUsers.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApplicationUserExists(string id)
        {
            return _context.AppUsers.Any(e => e.Id == id);
        }
    }
}
