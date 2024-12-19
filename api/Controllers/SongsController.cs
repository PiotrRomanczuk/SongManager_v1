using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SongsAPI.Data;
using SongsAPI.Models;
using SongsAPI.Services;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.Extensions.Logging;

namespace SongsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly SongImportService _importService;
        private readonly ILogger<SongsController> _logger;

        public SongsController(
            ApplicationDbContext context,
            SongImportService importService,
            ILogger<SongsController> logger)
        {
            _context = context;
            _importService = importService;
            _logger = logger;
        }

        // GET: api/Songs
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
        {
            return await _context.Songs
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();
        }

        // GET: api/Songs/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Song>> GetSong(Guid id)
        {
            var song = await _context.Songs.FindAsync(id);

            if (song == null)
            {
                return NotFound();
            }

            return song;
        }

        // POST: api/Songs
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Song>> PostSong(Song song)
        {
            song.CreatedAt = DateTime.UtcNow;
            _context.Songs.Add(song);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSong), new { id = song.Id }, song);
        }

        // POST: api/Songs/import
        [HttpPost("import")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ImportSongs()
        {
            try
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "songs_rows.csv");
                if (!System.IO.File.Exists(filePath))
                {
                    return BadRequest("CSV file not found");
                }

                await _importService.ImportSongsFromCsv(filePath);
                return Ok("Songs imported successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error importing songs");
                return StatusCode(500, "Error importing songs");
            }
        }

        // PUT: api/Songs/{id}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutSong(Guid id, Song song)
        {
            if (id != song.Id)
            {
                return BadRequest();
            }

            _context.Entry(song).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(id))
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

        // DELETE: api/Songs/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteSong(Guid id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SongExists(Guid id)
        {
            return _context.Songs.Any(e => e.Id == id);
        }
    }
}
