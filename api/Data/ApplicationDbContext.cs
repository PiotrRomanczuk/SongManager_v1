using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SongsAPI.Models;
using SongsAPI.Models.Users;

namespace SongsAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> // Changed from Student to ApplicationUser
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> AppUsers { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Lesson> Lessons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure ApplicationUser entity
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable("AppUsers");
                entity.HasKey(e => e.Id);
            });

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
                .HasOne(l => l.Teacher)
                .WithMany(t => t.Lessons)
                .HasForeignKey(l => l.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Teacher Id as primary key
            modelBuilder.Entity<Teacher>()
                .HasKey(t => t.Id);
            // .WithMany(student => student.Teachers);
        }
    }
}
