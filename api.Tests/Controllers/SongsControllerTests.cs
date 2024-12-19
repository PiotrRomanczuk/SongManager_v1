using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SongsAPI.Controllers;
using SongsAPI.Models;
using SongsAPI.Services;
using Xunit;

namespace SongsAPI.Tests.Controllers
{
    public class SongsControllerTests : TestBase
    {
        private readonly SongsController _controller;
        private readonly Mock<ILogger<SongsController>> _mockLogger;

        public SongsControllerTests()
        {
            _mockLogger = new Mock<ILogger<SongsController>>();
            _controller = new SongsController(_context, _songImportService, _mockLogger.Object);
        }

        [Fact]
        public async Task GetSongs_ReturnsAllSongs()
        {
            // Arrange
            var songs = new List<Song>
            {
                new Song { Id = Guid.NewGuid(), Title = "Song 1", CreatedAt = DateTime.UtcNow },
                new Song { Id = Guid.NewGuid(), Title = "Song 2", CreatedAt = DateTime.UtcNow }
            };
            await _context.Songs.AddRangeAsync(songs);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetSongs();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Song>>>(result);
            var returnedSongs = Assert.IsAssignableFrom<IEnumerable<Song>>(actionResult.Value);
            Assert.Equal(2, returnedSongs.Count());
        }

        [Fact]
        public async Task GetSong_ValidId_ReturnsSong()
        {
            // Arrange
            var song = new Song
            {
                Id = Guid.NewGuid(),
                Title = "Test Song",
                Level = "Beginner",
                CreatedAt = DateTime.UtcNow
            };
            await _context.Songs.AddAsync(song);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetSong(song.Id);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Song>>(result);
            var returnedSong = Assert.IsType<Song>(actionResult.Value);
            Assert.Equal(song.Title, returnedSong.Title);
        }

        [Fact]
        public async Task GetSong_InvalidId_ReturnsNotFound()
        {
            // Act
            var result = await _controller.GetSong(Guid.NewGuid());

            // Assert
            var actionResult = Assert.IsType<ActionResult<Song>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task PutSong_ValidUpdate_ReturnsNoContent()
        {
            // Arrange
            var song = new Song
            {
                Id = Guid.NewGuid(),
                Title = "Original Title",
                Level = "Beginner",
                CreatedAt = DateTime.UtcNow
            };
            await _context.Songs.AddAsync(song);
            await _context.SaveChangesAsync();

            var updatedSong = new Song
            {
                Id = song.Id,
                Title = "Updated Title",
                Level = "Advanced",
                CreatedAt = DateTime.UtcNow
            };

            // Act
            var result = await _controller.PutSong(song.Id, updatedSong);

            // Assert
            Assert.IsType<NoContentResult>(result);
            var dbSong = await _context.Songs.FindAsync(song.Id);
            Assert.NotNull(dbSong);
            Assert.Equal("Updated Title", dbSong.Title);
            Assert.Equal("Advanced", dbSong.Level);
        }

        [Fact]
        public async Task DeleteSong_ValidId_RemovesSong()
        {
            // Arrange
            var song = new Song
            {
                Id = Guid.NewGuid(),
                Title = "Test Song",
                CreatedAt = DateTime.UtcNow
            };
            await _context.Songs.AddAsync(song);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.DeleteSong(song.Id);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Null(await _context.Songs.FindAsync(song.Id));
        }
    }
}
