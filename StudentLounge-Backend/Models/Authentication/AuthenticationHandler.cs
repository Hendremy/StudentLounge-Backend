using Microsoft.AspNetCore.Identity;

namespace StudentLounge_Backend.Models.Authentication
{
    public class AuthenticationHandler : IHandleAuth
    {
        private readonly IHandleUsers _userRepo;
        public AuthenticationHandler(IHandleUsers userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<AppUser> Login(UserLogin loginInfo)
        {
            return await _userRepo.LoginUserAsync(loginInfo.Username, loginInfo.Password);
        }

        public async Task<AppUser> Register(UserRegister registerInfo)
        {
            AppUser createdUser = null;
            if (registerInfo.Password == registerInfo.PasswordRep)
            {
                var userExists = await _userRepo.UserExistsAsync(registerInfo.Email);
                if (!userExists) { 
                    var user = CreateUser(registerInfo);
                    var registerResult = await _userRepo.CreateUserAsync(user, registerInfo.Password);
                    if(registerResult.Succeeded)
                    {
                        createdUser = await _userRepo.LoginUserAsync(registerInfo.Email, registerInfo.Password);
                    }
                }
            }
            return createdUser;
        }

        private AppUser CreateUser(UserRegister register)
        {
            return new AppUser()
            {
                Email = register.Email,
                UserName = register.Email,
                Lastname = register.Lastname,
                Firstname = register.Firstname
            };
        }
    }
}
