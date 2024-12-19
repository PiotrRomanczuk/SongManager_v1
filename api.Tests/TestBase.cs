using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SongsAPI.Data;
using SongsAPI.Services;

namespace SongsAPI.Tests
{
    public class TestBase : IDisposable
    {
        protected readonly ApplicationDbContext _context;
        protected readonly Mock<ILogger<SongImportService>> _mockLogger;
        protected readonly SongImportService _songImportService;

        public TestBase()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _mockLogger = new Mock<ILogger<SongImportService>>();
            _songImportService = new SongImportService(_context, _mockLogger.Object);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
