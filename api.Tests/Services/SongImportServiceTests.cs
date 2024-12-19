using System.Text;
using Microsoft.EntityFrameworkCore;
using SongsAPI.Models;
using Xunit;

namespace SongsAPI.Tests.Services
{
    public class SongImportServiceTests : TestBase
    {
        [Fact]
        public async Task ImportSongsFromCsv_ValidData_ShouldImportSuccessfully()
        {
            // Arrange
            var csvContent = @"id,title,level,key,chords,audio_files,created_at,author,Ultimate-Guitar Link,shortTitle
00469467-bd4d-4350-8dcc-c938f3ef60da,Test Song,Beginner,C,,,,Test Author,,";

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(csvContent));

            // Act
            await _songImportService.ImportSongsFromCsv(stream);

            // Assert
            var songs = await _context.Songs.ToListAsync();
            Assert.Single(songs);
            var song = songs[0];
            Assert.Equal("Test Song", song.Title);
            Assert.Equal("Beginner", song.Level);
            Assert.Equal("C", song.SongKey);
            Assert.Equal("Test Author", song.Author);
        }

        [Fact]
        public async Task ImportSongsFromCsv_DuplicateTitle_ShouldUpdateExisting()
        {
            // Arrange
            var existingSong = new Song
            {
                Id = Guid.NewGuid(),
                Title = "Test Song",
                Level = "Advanced",
                SongKey = "D",
                Author = "Original Author",
                CreatedAt = DateTime.UtcNow
            };
            await _context.Songs.AddAsync(existingSong);
            await _context.SaveChangesAsync();

            var csvContent = @"id,title,level,key,chords,audio_files,created_at,author,Ultimate-Guitar Link,shortTitle
00469467-bd4d-4350-8dcc-c938f3ef60da,Test Song,Beginner,C,,,,New Author,,";

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(csvContent));

            // Act
            await _songImportService.ImportSongsFromCsv(stream);

            // Assert
            var songs = await _context.Songs.ToListAsync();
            Assert.Single(songs);
            var song = songs[0];
            Assert.Equal("Test Song", song.Title);
            Assert.Equal("Beginner", song.Level);
            Assert.Equal("C", song.SongKey);
            Assert.Equal("New Author", song.Author);
        }

        [Fact]
        public async Task ImportSongsFromCsv_InvalidId_ShouldGenerateNewId()
        {
            // Arrange
            var csvContent = @"id,title,level,key,chords,audio_files,created_at,author,Ultimate-Guitar Link,shortTitle
invalid-guid,Test Song,Beginner,C,,,,Test Author,,";

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(csvContent));

            // Act
            await _songImportService.ImportSongsFromCsv(stream);

            // Assert
            var songs = await _context.Songs.ToListAsync();
            Assert.Single(songs);
            var song = songs[0];
            Assert.NotEqual(Guid.Empty, song.Id);
        }

        [Fact]
        public async Task ImportSongsFromCsv_InvalidDate_ShouldUseCurrentDate()
        {
            // Arrange
            var csvContent = @"id,title,level,key,chords,audio_files,created_at,author,Ultimate-Guitar Link,shortTitle
00469467-bd4d-4350-8dcc-c938f3ef60da,Test Song,Beginner,C,,,invalid-date,Test Author,,";

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(csvContent));

            // Act
            await _songImportService.ImportSongsFromCsv(stream);

            // Assert
            var songs = await _context.Songs.ToListAsync();
            Assert.Single(songs);
            var song = songs[0];
            Assert.True((DateTime.UtcNow - song.CreatedAt).TotalMinutes < 1);
        }
    }
}
