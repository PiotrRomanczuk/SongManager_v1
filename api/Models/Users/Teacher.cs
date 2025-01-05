using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SongsAPI.Models.Users
{
    public class Teacher : ApplicationUser
    {
        [Key]
        public new int Id { get; set; } // Change Id type to int

        [Required]
        [StringLength(100)]
        public new string Name { get; set; } = string.Empty;

        // Navigation property for lessons
        public new ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
