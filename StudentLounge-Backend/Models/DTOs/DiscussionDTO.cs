using StudentLounge_Backend.Models.Tutorats;

namespace StudentLounge_Backend.Models.DTOs
{
    public class DiscussionDTO
    {
        public int Id { get; private set; }
        public string Tutoredname { get; private set; }
        public string? Tutoredimage { get; private set; }
        public string? Tutorimage { get; private set; }
        public string? Tutorname { get; private set; }

        public DiscussionDTO(Tutoring tutoring)
        {
            Id = tutoring.Id;
            Tutoredname = tutoring.Tutored.Fullname;
            Tutoredimage = tutoring.Tutored.Image;
            Tutorimage = tutoring.Tutor.Image;
            Tutorname = tutoring.Tutor?.Fullname;
        }
    }
}
