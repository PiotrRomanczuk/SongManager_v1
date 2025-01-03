using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SongsAPI.Models.Users
{
    public class Student : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        // Navigation property for songs
        public virtual ICollection<Song> FavoriteSongs { get; set; } = new List<Song>();

        // Navigation property for lessons
        public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    }
}
