using Microsoft.AspNetCore.Identity;

namespace StudentLounge_Backend.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string? Image { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public List<Lesson> Lessons { get; set; } = new List<Lesson>();

    }
}
