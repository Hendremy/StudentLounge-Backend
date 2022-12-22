using StudentLounge_Backend.Models.Tutorats;
using static StudentLounge_Backend.Models.DTOs.ValidatedTutoringDTO;

namespace StudentLounge_Backend.Models.DTOs
{
    public class TutoringRequestDTO
    {
        public int Id { get; set; }
        public TutoringUserDTO Tutored { get; set; }
        public TutoringUserDTO? Tutor { get; set; }
        public string Lesson { get; set; }

        public TutoringRequestDTO(Tutoring tutoring)
        {
            var tutor = tutoring.Tutor;
            var tutored = tutoring.Tutored;
            var lesson = tutoring.Lesson;
            Tutored = new TutoringUserDTO(tutored.Fullname, tutored.Image);
            Tutor = tutor is null ? null : new TutoringUserDTO(tutor.Fullname, tutor.Image);
            Lesson = lesson.Name;
            Id = tutoring.Id;
        }
    }
}
