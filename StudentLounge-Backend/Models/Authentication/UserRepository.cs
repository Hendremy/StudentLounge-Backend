using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace StudentLounge_Backend.Models.Authentication
{
    public class UserRepository : IHandleUsers
    {

        private UserManager<AppUser> _userManager;

        public UserRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task<IdentityResult> CreateUserAsync(AppUser user, string password)
        {
            var createResult = await _userManager.CreateAsync(user, password);
            if (createResult.Succeeded)
            {
                return await _userManager.AddToRoleAsync(user, "Student");
            }
            return createResult;
        }

        public async Task<IdentityResult> UpdateUser(AppUser user, string? password)
        {
            if (user != null)
            {
                if(password != null)
                {
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, password);
                }
                return await _userManager.UpdateAsync(user);
            }else
            {
                return null;
            }
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user != null;
        }

        public async Task<AppUser> LoginUserAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if(user != null)
            {
                var loginResult = await _userManager.CheckPasswordAsync(user, password);
                var lockout = await _userManager.IsLockedOutAsync(user);

                if (loginResult && !lockout)
                {
                    return user;
                }
            }
            return null;
        }

        public async Task<IdentityResult> CreateExternalUserAsync(string providerName, string userId, AppUser user)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled)) 
            {
                var createResult = await _userManager.CreateAsync(user);
                var assignRole = await AddToStudentRole(user);
                var addLogin = await AddExternalLogin(providerName, userId, user);
                if(createResult.Succeeded && assignRole.Succeeded && addLogin.Succeeded)
                {
                    scope.Complete();
                    return IdentityResult.Success;
                }
                else
                {
                    scope.Dispose();
                    return IdentityResult.Failed();
                }
            }
        }

        public async Task<IEnumerable<string>> GetUserRoles(AppUser user)
        {
            return await _userManager.GetRolesAsync(user);
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

        public async Task<bool> BlockUser(AppUser user)
        {
            await _userManager.SetLockoutEnabledAsync(user, true);
            await _userManager.SetLockoutEndDateAsync(user, DateTime.Today.AddYears(100));
            return await _userManager.IsLockedOutAsync(user);
        }
    }
}
