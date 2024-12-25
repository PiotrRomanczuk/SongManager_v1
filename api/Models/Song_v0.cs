// using System.ComponentModel.DataAnnotations;
// using System.Collections.Generic;

// namespace SongsAPI.Models
// {
//     public class Song_v0
//     {
//         public int Id { get; set; }

//         [Required(ErrorMessage = "Tytuł jest wymagany")]
//         public string Title { get; set; } = string.Empty;

//         public string? Artist { get; set; }

//         [Required(ErrorMessage = "Grupa jest wymagana")]
//         public string ClassGroup { get; set; } = string.Empty;

//         public DateTime DateAdded { get; set; } = DateTime.Now;

//         // Navigation property for students who favorited this song
//         public ICollection<Student> FavoriteByStudents { get; set; } = new List<Student>();

//         // Navigation property for lessons this song belongs to
//         public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
//     }
// }
