using StudentLounge_Backend.Models.Tutorats;

namespace StudentLounge_Backend.Models.DTOs
{
    public class ValidatedTutoringDTO
    {
        public int Id { get; set; }
        public TutoringUser Tutored { get; set; }
        public TutoringUser Tutor { get; set; }
        public string Lesson { get; set; }

        public ValidatedTutoringDTO(Tutoring tutoring)
        {
            if (tutoring.Tutor is null) throw new InvalidOperationException("Tutoring has no Tutor.");
            var tutor = tutoring.Tutor;
            var tutored = tutoring.Tutored;
            var lesson = tutoring.Lesson;
            Tutored = new TutoringUser(tutored.Fullname, tutored.Image);
            Tutor = new TutoringUser(tutor.Fullname, tutor.Image);
            Lesson = lesson.Name;
            Id = tutoring.Id;
        }

        public class TutoringUser
        {
            public string Name { get; set; }
            public string? Image { get; set; }

            public TutoringUser(string name, string? image)
            {
                Name = name;
                Image = image;
            }
        }
    }
}
