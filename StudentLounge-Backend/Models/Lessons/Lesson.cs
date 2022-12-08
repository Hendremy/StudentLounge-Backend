using StudentLounge_Backend.Models.Files;
using StudentLounge_Backend.Models.Tutorats;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StudentLounge_Backend.Models
{
    public class Lesson
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<AppUser> Users { get; set; }

        [JsonIgnore]
        public virtual ICollection<LessonFile> Files { get; set; }

        [JsonIgnore]
        public virtual ICollection<Tutoring> Tutorats { get; set; }
    }
}
