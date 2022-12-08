using StudentLounge_Backend.Models.Tutorats;

namespace StudentLounge_Backend.Models.DTOs
{
    public class TutoringDTO
    {
        public int Id { get; private set; }
        public object User { get; private set; }

        public TutoringDTO(Tutoring tutoring)
        {
            Id = tutoring.Id;
            User = new { tutoring.Tutored.Fullname, tutoring.Tutored.Image };
        }
    }
}
