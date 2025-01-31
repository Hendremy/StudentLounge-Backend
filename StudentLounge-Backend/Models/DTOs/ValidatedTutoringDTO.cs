﻿using StudentLounge_Backend.Models.Tutorats;

namespace StudentLounge_Backend.Models.DTOs
{
    public class ValidatedTutoringDTO
    {
        public int Id { get; set; }
        public TutoringUserDTO Tutored { get; set; }
        public TutoringUserDTO Tutor { get; set; }
        public string Lesson { get; set; }

        public ValidatedTutoringDTO(Tutoring tutoring)
        {
            if (tutoring.Tutor is null) throw new InvalidOperationException("Tutoring has no Tutor.");
            var tutor = tutoring.Tutor;
            var tutored = tutoring.Tutored;
            var lesson = tutoring.Lesson;
            Tutored = new TutoringUserDTO(tutored.Fullname, tutored.Image);
            Tutor = new TutoringUserDTO(tutor.Fullname, tutor.Image);
            Lesson = lesson.Name;
            Id = tutoring.Id;
        }
    }
}
