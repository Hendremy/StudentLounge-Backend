using Microsoft.AspNetCore.Identity;
using StudentLounge_Backend.Models.Files;
using System.Text.Json.Serialization;

namespace StudentLounge_Backend.Models
{
    public class AppUser : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public string? Image { get; set; }

        public string Fullname => $"{Firstname} {Lastname}";

        [JsonIgnore]
        public List<Lesson> Lessons { get; set; } = new List<Lesson>();
        [JsonIgnore]
        public List<LessonFile> PostedFiles { get; set; } = new List<LessonFile>();

    }
}
