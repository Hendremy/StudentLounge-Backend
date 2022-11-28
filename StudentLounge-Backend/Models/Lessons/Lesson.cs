using StudentLounge_Backend.Models.Files;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StudentLounge_Backend.Models
{
    public class Lesson
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<AppUser> Users { get; set; }

        [JsonIgnore]
        public ICollection<LessonFile> Files { get; set; }
    }
}
