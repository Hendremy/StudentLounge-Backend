using Microsoft.AspNetCore.Identity;

namespace StudentLounge_Backend.Models.Authentication
{
    public interface ICreateToken
    {
        public string Create(AppUser user, IEnumerable<string> roles);
    }
}
