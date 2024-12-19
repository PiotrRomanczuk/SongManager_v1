using System.ComponentModel.DataAnnotations;

namespace SongsAPI.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        // Navigation property for lessons
        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    }
}
