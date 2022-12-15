using StudentLounge_Backend.Models.Tutorats;

namespace StudentLounge_Backend.Models.DTOs
{
    public class UserDTO
    {
        public string Id { get; private set; }
        public string Fullname { get; private set; }
        public string? Image { get; private set; }

        public UserDTO(AppUser user)
        {
            Id = user.Id;
            Fullname = user.Fullname;
            Image = user.Image;
        }
    }
}
