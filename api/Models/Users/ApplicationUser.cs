using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SongsAPI.Models.Users;
public class ApplicationUser : IdentityUser
{
    // override the Id property from IdentityUser
    [Key]
    public override string Id { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public override string? Email { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;

    public bool IsAdmin { get; set; }

    public virtual ICollection<Song> FavoriteSongs { get; set; } = new List<Song>();

    // // Navigation property for lessons
    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

}
