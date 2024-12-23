using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SongsAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace SongsAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Song>? Songs { get; set; }
        public DbSet<Student>? Students { get; set; }
        public DbSet<Teacher>? Teachers { get; set; }
        public DbSet<Lesson>? Lessons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure many-to-many relationship between Student and Song
            modelBuilder.Entity<Student>()
                .HasMany(s => s.FavoriteSongs)
                .WithMany(s => s.FavoriteByStudents)
                .UsingEntity(j => j.ToTable("StudentSongs"));

            // Configure many-to-many relationship between Student and Lesson
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Lessons)
                .WithMany(l => l.Students)
                .UsingEntity(j => j.ToTable("StudentLessons"));

            // Configure many-to-many relationship between Lesson and Song
            modelBuilder.Entity<Lesson>()
                .HasMany(l => l.Songs)
                .WithMany(s => s.Lessons)
                .UsingEntity(j => j.ToTable("LessonSongs"));

            // Configure one-to-many relationship between Teacher and Lesson
            modelBuilder.Entity<Lesson>()
                .HasOne(t => t.Teacher)
                .WithMany(l => l.Lessons)
                .HasForeignKey(t => t.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
