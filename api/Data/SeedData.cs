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
            if (context.Songs.Any())
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
        }
    }
}
