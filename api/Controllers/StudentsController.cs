using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SongsAPI.Data;
using SongsAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace SongsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Student> _userManager;

        public StudentsController(ApplicationDbContext context, UserManager<Student> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _userManager.Users.ToListAsync();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(string id)
        {
            var student = await _userManager.FindByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(string id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            var existingStudent = await _userManager.FindByIdAsync(id);
            if (existingStudent == null)
            {
                return NotFound();
            }

            existingStudent.Name = student.Name;
            var result = await _userManager.UpdateAsync(existingStudent);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return NoContent();
        }

        // POST: api/Students/AddFavoriteSong/5
        [HttpPost("AddFavoriteSong/{songId}")]
        public async Task<IActionResult> AddFavoriteSong(int songId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var student = await _userManager.FindByIdAsync(userId);
            if (student == null)
            {
                return NotFound("Student not found");
            }

            var song = await _context.Songs.FindAsync(songId);
            if (song == null)
            {
                return NotFound("Song not found");
            }

            student.FavoriteSongs.Add(song);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Students/RemoveFavoriteSong/5
        [HttpDelete("RemoveFavoriteSong/{songId}")]
        public async Task<IActionResult> RemoveFavoriteSong(int songId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var student = await _userManager.FindByIdAsync(userId);
            if (student == null)
            {
                return NotFound("Student not found");
            }

            var song = await _context.Songs.FindAsync(songId);
            if (song == null)
            {
                return NotFound("Song not found");
            }

            student.FavoriteSongs.Remove(song);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
