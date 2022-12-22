using StudentLounge_Backend.Models.Tutorats;

namespace StudentLounge_Backend.Models.DTOs
{
    public class LessonDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public TutoringRequestDTO? Tutoring { get; set; }
        public LessonDTO(Lesson lesson, Tutoring? userTutoring) 
        {
            Id = lesson.Id;
            Name = lesson.Name;
            Tutoring = userTutoring is null ? null : new TutoringRequestDTO(userTutoring);
        }
    }
}
