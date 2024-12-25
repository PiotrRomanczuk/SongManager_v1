using System.ComponentModel.DataAnnotations;

namespace SongsAPI.Models.Users
{
    public class Lesson
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public DateTime DateTime { get; set; }

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        // Foreign key for Teacher
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;

        // Navigation property for students
        public ICollection<Student> Students { get; set; } = new List<Student>();

        // Navigation property for songs in this lesson
        public ICollection<Song> Songs { get; set; } = new List<Song>();
    }
}
