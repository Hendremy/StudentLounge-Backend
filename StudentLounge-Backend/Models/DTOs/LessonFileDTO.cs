using StudentLounge_Backend.Models.Files;

namespace StudentLounge_Backend.Models.DTOs
{
    public class LessonFileDTO
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string User { get; private set; }
        public DateTime Date { get; private set; }
        public LessonFileType Type { get; private set; }

        public LessonFileDTO(LessonFile lessonFile)
        {
            Id = lessonFile.Id;
            Name = lessonFile.FileName;
            User = lessonFile.Author.Fullname;
            Date = lessonFile.AddedOn;
            Type = lessonFile.Type;
        }
    }
}
