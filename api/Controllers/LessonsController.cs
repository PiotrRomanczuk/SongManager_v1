using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SongsAPI.Data;
using SongsAPI.Models;

namespace SongsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Student> _userManager;

        public LessonsController(ApplicationDbContext context, UserManager<Student> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Lessons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lesson>>> GetLessons()
        {
            return await _context.Lessons
                .Include(l => l.Teacher)
                .Include(l => l.Students)
                .Include(l => l.Songs)
                .ToListAsync();
        }

        // GET: api/Lessons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lesson>> GetLesson(int id)
        {
            var lesson = await _context.Lessons
                .Include(l => l.Teacher)
                .Include(l => l.Students)
                .Include(l => l.Songs)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lesson == null)
            {
                return NotFound();
            }

            return lesson;
        }

        // POST: api/Lessons
        [HttpPost]
        public async Task<ActionResult<Lesson>> PostLesson(Lesson lesson)
        {
            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLesson), new { id = lesson.Id }, lesson);
        }

        // PUT: api/Lessons/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLesson(int id, Lesson lesson)
        {
            if (id != lesson.Id)
            {
                return BadRequest();
            }

            _context.Entry(lesson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LessonExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Lessons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesson(int id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }

            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Lessons/5/students/3
        [HttpPost("{lessonId}/students/{studentId}")]
        public async Task<IActionResult> AddStudentToLesson(int lessonId, string studentId)
        {
            var lesson = await _context.Lessons
                .Include(l => l.Students)
                .FirstOrDefaultAsync(l => l.Id == lessonId);

            if (lesson == null)
            {
                return NotFound("Lesson not found");
            }

            var student = await _userManager.FindByIdAsync(studentId);
            if (student == null)
            {
                return NotFound("Student not found");
            }

            if (!lesson.Students.Any(s => s.Id == studentId))
            {
                lesson.Students.Add(student);
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }

        // DELETE: api/Lessons/5/students/3
        [HttpDelete("{lessonId}/students/{studentId}")]
        public async Task<IActionResult> RemoveStudentFromLesson(int lessonId, string studentId)
        {
            var lesson = await _context.Lessons
                .Include(l => l.Students)
                .FirstOrDefaultAsync(l => l.Id == lessonId);

            if (lesson == null)
            {
                return NotFound("Lesson not found");
            }

            var student = lesson.Students.FirstOrDefault(s => s.Id == studentId);
            if (student == null)
            {
                return NotFound("Student not found in this lesson");
            }

            lesson.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Lessons/5/songs/3
        [HttpPost("{lessonId}/songs/{songId}")]
        public async Task<IActionResult> AddSongToLesson(int lessonId, Guid songId)
        {
            var lesson = await _context.Lessons
                .Include(l => l.Songs)
                .FirstOrDefaultAsync(l => l.Id == lessonId);

            if (lesson == null)
            {
                return NotFound("Lesson not found");
            }

            var song = await _context.Songs.FindAsync(songId);

            if (song == null)
            {
                return NotFound("Song not found");
            }

            if (lesson.Songs.Any(s => s.Id == songId))
            {
                return BadRequest("Song is already added to this lesson");
            }

            lesson.Songs.Add(song);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Lessons/5/songs/3
        [HttpDelete("{lessonId}/songs/{songId}")]
        public async Task<IActionResult> RemoveSongFromLesson(int lessonId, Guid songId)
        {
            var lesson = await _context.Lessons
                .Include(l => l.Songs)
                .FirstOrDefaultAsync(l => l.Id == lessonId);

            if (lesson == null)
            {
                return NotFound("Lesson not found");
            }

            var song = lesson.Songs.FirstOrDefault(s => s.Id == songId);
            if (song == null)
            {
                return NotFound("Song not found in this lesson");
            }

            lesson.Songs.Remove(song);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LessonExists(int id)
        {
            return _context.Lessons.Any(e => e.Id == id);
        }
    }
}
