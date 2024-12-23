using System;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using SongsAPI.Models;
using SongsAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace SongsAPI.Services
{
    public class SongImportService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SongImportService> _logger;

        public SongImportService(ApplicationDbContext context, ILogger<SongImportService> logger)
        {
            _context = context;
            _logger = logger;
        }

        private CsvConfiguration GetCsvConfiguration()
        {
            return new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null,
                BadDataFound = null
            };
        }

        private async Task ImportSongs(IEnumerable<Song> records)
        {
            foreach (var song in records)
            {
                song.Id = song.Id == Guid.Empty ? Guid.NewGuid() : song.Id;
                song.CreatedAt = song.CreatedAt == default ? DateTime.UtcNow : song.CreatedAt;


                if (_context.Songs == null)
                {
                    _logger.LogError("Songs DbSet is null");
                    throw new InvalidOperationException("Songs DbSet is null");
                }

                var existingSong = await _context.Songs
                    .FirstOrDefaultAsync(s => s.Title == song.Title);

                if (existingSong == null)
                {
                    await _context.Songs.AddAsync(song);
                    _logger.LogInformation($"Adding new song: {song.Title}");
                }
                else
                {
                    existingSong.Level = song.Level;
                    existingSong.SongKey = song.SongKey;
                    existingSong.Author = song.Author;
                    existingSong.UltimateGuitarLink = song.UltimateGuitarLink;
                    _logger.LogInformation($"Updating existing song: {song.Title}");
                }
            }

            await _context.SaveChangesAsync();
            _logger.LogInformation($"Successfully imported {records.Count()} songs");
        }

        public async Task ImportSongsFromCsv(string filePath)
        {
            try
            {
                using var reader = new StreamReader(filePath);
                using var csv = new CsvReader(reader, GetCsvConfiguration());
                csv.Context.RegisterClassMap<SongMap>();

                var records = csv.GetRecords<Song>().ToList();
                await ImportSongs(records);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error importing songs from CSV file");
                throw;
            }
        }

        public async Task ImportSongsFromCsv(Stream stream)
        {
            try
            {
                using var reader = new StreamReader(stream);
                using var csv = new CsvReader(reader, GetCsvConfiguration());
                csv.Context.RegisterClassMap<SongMap>();

                var records = csv.GetRecords<Song>().ToList();
                await ImportSongs(records);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error importing songs from CSV stream");
                throw;
            }
        }

        public async Task<Song?> ImportSongAsync(string songTitle)
        {
            if (_context.Songs == null)
            {
                _logger.LogError("Songs DbSet is null");
                throw new InvalidOperationException("Songs DbSet is null");
            }

            var song = await _context.Songs
                .FirstOrDefaultAsync(s => s.Title == songTitle);

            if (song == null)
            {
                return null;
            }

            // ...existing code...

            return song;
        }
    }
}
