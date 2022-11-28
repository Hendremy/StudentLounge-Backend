using StudentLounge_Backend.Models.Files;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StudentLounge_Backend.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public IEnumerable<AppUser> Users { get; set; } = new List<AppUser>();

        [JsonIgnore]
        public IEnumerable<LessonFile> Files { get; set; } = new List<LessonFile>();
    }
}
