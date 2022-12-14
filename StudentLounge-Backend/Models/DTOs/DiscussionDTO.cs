using StudentLounge_Backend.Models.Tutorats;

namespace StudentLounge_Backend.Models.DTOs
{
    public class DiscussionDTO
    {
        public int Id { get; private set; }
        public string? Name { get; private set; }
        public string? Image { get; private set; }

        public DiscussionDTO(Tutoring tutoring, string userId)
        {
            this.Id = tutoring.Id;
            if(tutoring.Tutor?.Id == userId)
            {
                this.Name = tutoring.Tutored.Fullname;
                this.Image = tutoring.Tutored.Image;
            }
            else
            {
                this.Name = tutoring.Tutor?.Fullname;
                this.Image = tutoring.Tutor?.Image;
            }
        }
    }
}
