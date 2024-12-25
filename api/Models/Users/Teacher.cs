using System.ComponentModel.DataAnnotations;

namespace SongsAPI.Models.Users;

public class Teacher : User
{
    public new int Id { get; set; }

    [Required]
    [StringLength(100)]
    public new string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public new string Email { get; set; } = string.Empty;

    public Boolean isAdmin { get; set; } = true;

    // Navigation property for lessons
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}

