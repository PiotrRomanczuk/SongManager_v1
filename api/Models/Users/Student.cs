using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SongsAPI.Models.Users
{
    public class Student : User
    {
        [Required]
        [StringLength(100)]
        public new string Name { get; set; } = string.Empty;
        public Boolean isAdmin { get; set; } = false;


        // Navigation property for songs
        public virtual ICollection<Song> FavoriteSongs { get; set; } = new List<Song>();

        // Navigation property for lessons
        public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    }
}
