using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StudentLounge_Backend.Models.Files
{
    public class LessonFile
    {
        [Key]
        public string Id { get; set; }
        public AppUser Author { get; set; }
        public string Name { get; set; }
        public DateTime AddedOn { get; set; }
        public string Path { get; set; }
        public LessonFileType Type { get; set; }
        [JsonIgnore]
        public Lesson Lesson { get; set; }

        public LessonFile(AppUser author, string name, string path, LessonFileType type, Lesson lesson)
        {
            Author = author;
            Name = name;
            Path = path;
            Type = type;
            AddedOn = DateTime.Now;
            Lesson = lesson;
        }
    }

    public enum LessonFileType
    {
        Summary, Notes
    }
}
