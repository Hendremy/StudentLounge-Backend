using Microsoft.AspNetCore.Identity;

namespace StudentLounge_Backend.Models.Authentication
{
    public interface ICreateUserInfo
    {
        UserInfo Create(ICreateToken tokenCreator, AppUser user, IEnumerable<string> roles);
    }
}
