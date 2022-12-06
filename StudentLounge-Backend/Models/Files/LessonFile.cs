using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StudentLounge_Backend.Models.Files
{
    public class LessonFile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public virtual AppUser Author { get; set; }
        public string FileName { get; set; }
        public DateTime AddedOn { get; set; }
        public string FilePath { get; set; }
        public LessonFileType Type { get; set; }
        [JsonIgnore]
        public virtual Lesson Lesson { get; set; }

        public LessonFile() { }

        public LessonFile(AppUser author, string name, string path, LessonFileType type, Lesson lesson)
        {
            Author = author;
            FileName = name;
            FilePath = path;
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
