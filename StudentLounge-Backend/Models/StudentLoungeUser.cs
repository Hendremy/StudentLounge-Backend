using Microsoft.AspNetCore.Identity;

namespace StudentLounge_Backend.Models
{
    public class StudentLoungeUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ExternalAccount? ExtAccount { get; set; }
        public string FullName => $"{FirstName} {LastName}";

    }
}
