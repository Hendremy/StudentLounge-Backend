using Microsoft.AspNetCore.Identity;
using StudentLounge_Backend.Models.Files;
using StudentLounge_Backend.Models.Tutorats;
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
        public virtual List<Lesson> Lessons { get; set; } = new List<Lesson>();

        [JsonIgnore]
        public virtual List<LessonFile> PostedFiles { get; set; } = new List<LessonFile>();

        [JsonIgnore]
        public virtual ICollection<Tutoring> TutoratAccepted { get; set; } = new List<Tutoring>();

        [JsonIgnore]
        public virtual ICollection<Tutoring> TutoratAsked { get; set; } = new List<Tutoring>();

    }
}
