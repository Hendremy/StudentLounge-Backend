using Microsoft.AspNetCore.Identity;
using System.Transactions;

namespace StudentLounge_Backend.Models
{
    public class UserRepository : IHandleUsers
    {

        private UserManager<AppUser> _userManager;

        public UserRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public Task<AppUser> CreateUserAsync()
        {
            throw new NotImplementedException();
        }


        public async Task<AppUser> CreateExternalUserAsync(string providerName, string userId, AppUser user)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled)) 
            {
                var createResult = await _userManager.CreateAsync(user);
                var assignRole = await AddToStudentRole(user);
                var addLogin = await AddExternalLogin(providerName, userId, user);
                if(createResult.Succeeded && assignRole.Succeeded && addLogin.Succeeded)
                {
                    scope.Complete();
                    return user;
                }
                else
                {
                    scope.Dispose();
                    return null;
                }
            }
        }

        private async Task<IdentityResult> AddExternalLogin(string providername, string userId, AppUser user)
        {
            var loginInfo = new UserLoginInfo(providername, userId, providername);
            return await _userManager.AddLoginAsync(user, loginInfo);
        }

        private async Task<IdentityResult> AddToStudentRole(AppUser user)
        {
            return await _userManager.AddToRoleAsync(user, "Student");
        }

        public async Task<AppUser> FindExternalUserAsync(string providerName, string userId)
        {
            return await _userManager.FindByLoginAsync(providerName, userId);
        }
    }
}
