using Microsoft.AspNetCore.Identity;

namespace StudentLounge_Backend.Models.Authentication
{
    public interface IHandleUsers
    {
        Task<AppUser> LoginUserAsync(string username, string password);
        Task<bool> UserExistsAsync(string username);
        Task<IdentityResult> CreateUserAsync(AppUser user, string password);
        Task<IdentityResult> CreateExternalUserAsync(string providerName, string userId, AppUser user);
        Task<AppUser> FindExternalUserAsync(string providerName, string userId);

        Task<IEnumerable<string>> GetUserRoles(AppUser user);
    }
}
