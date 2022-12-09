using StudentLounge_Backend.Models.Tutorats;

namespace StudentLounge_Backend.Models.DTOs
{
    public class TutoringDTO
    {
        public int Id { get; private set; }
        public string Username { get; private set; }
        public string? Userimage { get; private set; }
        public string? Tutorname { get; private set; }

        public TutoringDTO(Tutoring tutoring)
        {
            Id = tutoring.Id;
            Username = tutoring.Tutored.Fullname;
            Userimage = tutoring.Tutored.Image;
            Tutorname = tutoring.Tutor?.Fullname;
        }
    }
}
