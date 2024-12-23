using SongsAPI.Models;
using SongsAPI.Services;
using System.IO;

namespace SongsAPI.Data
{
    public static class SeedData
    {
        public static async Task Initialize(ApplicationDbContext context, SongImportService songImportService)
        {
            // Check if we already have songs
            if (context.Songs == null || context.Songs.Any())
            {
                return;   // DB has been seeded
            }

            // Import songs from CSV
            var csvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "songs_rows.csv");
            if (File.Exists(csvPath))
            {
                using var stream = File.OpenRead(csvPath);
                await songImportService.ImportSongsFromCsv(stream);
            }
            else
            {
                throw new FileNotFoundException($"CSV file not found at {csvPath}");
            }

            // Create ShortTitle for every song without spaces
            foreach (var song in context.Songs)
            {
                var shortTitle = GenerateShortTitle(song.Title);
                song.ShortTitle = string.IsNullOrEmpty(shortTitle) ? "Unknown" : shortTitle.Replace(" ", string.Empty);
            }

            await context.SaveChangesAsync();
        }

        private static string GenerateShortTitle(string title)
        {
            // Logic to generate a short title, e.g., first 10 characters
            return title.Length <= 10 ? title : title.Substring(0, 10);
        }
    }
}
