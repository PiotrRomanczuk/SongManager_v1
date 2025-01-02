using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SongsAPI.Models;
using SongsAPI.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace SongsAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
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
                .HasOne(l => l.Teacher)
                .WithMany(t => t.Lessons)
                .HasForeignKey(l => l.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}


// Konfiguracja tabeli Customer
// modelBuilder.Entity<Student>().HasDiscriminator<Student.Status>("CLASS_TYPE")
//                .HasValue<BaseClass>(BaseClass.Status.OK)
//                .HasValue<DerivedClass1>(BaseClass.Status.NOT1)
//                .HasValue<DerivedClass2>(BaseClass.Status.NOT2);