using Microsoft.AspNetCore.Identity;

namespace StudentLounge_Backend.Models
{
    public class AppUser : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public string? Image { get; set; }

        public string Fullname => $"{Firstname} {Lastname}";

        public List<Lesson> Lessons { get; set; } = new List<Lesson>();

    }
}
