using Microsoft.AspNetCore.Identity;

namespace StudentLounge_Backend.Models.Authentication
{
    public interface IHandleAuth
    {
        Task<AppUser> Register(UserRegister user);
        Task<AppUser> Login(UserLogin userLogin);
    }
}
