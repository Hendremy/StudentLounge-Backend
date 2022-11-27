using Microsoft.AspNetCore.Identity;

namespace StudentLounge_Backend.Models.Authentication
{
    public class UserInfoCreator : ICreateUserInfo
    {
        public UserInfo Create(ICreateToken tokenCreator, AppUser user, IEnumerable<string> roles)
        {
            string token = tokenCreator.Create(user, roles);
            return new UserInfo(user.Id, token, user.Fullname, user.Image, roles);
        }
    }
}
