namespace StudentLounge_Backend.Models
{
    public interface IHandleUsers
    {
        Task<AppUser> CreateUserAsync();
        Task<AppUser> CreateExternalUserAsync(string providerName, string userId, AppUser user);

        Task<AppUser> FindExternalUserAsync(string providerName, string userId);


    }
}
