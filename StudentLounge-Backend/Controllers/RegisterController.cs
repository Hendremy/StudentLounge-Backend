using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentLounge_Backend.Models;

namespace StudentLounge_Backend.Controllers
{
    //!!! Attention, avec Identity normal, serveur ne devient plus stateless, utiliser JWT
    [Route("[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly UserManager<StudentLoungeUser> _userManager;
        private readonly ICreateToken _jwtTokenCreator;

        public RegisterController([FromServices]UserManager<StudentLoungeUser> userManager, ICreateToken jwtTokenCreator)
        {
            _userManager = userManager;
            _jwtTokenCreator = jwtTokenCreator;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserRegister userRegister)
        {
            var existingUser = await FindUserByEmailAsync(userRegister.Email);
            if (userRegister.PassHash == userRegister.PassRepHash && existingUser == null) {
                var result = await CreateUserAsync(userRegister);
                if (result.Succeeded)
                {
                    var account = await FindUserByEmailAsync(userRegister.Email);
                    return Ok(_jwtTokenCreator.Create(account));
                }
                else {
                    return BadRequest(result.Errors);
                }
            }
            return BadRequest();
        }


        private async Task<IdentityResult> CreateUserAsync(UserRegister userInfo)
        {
            var user = new StudentLoungeUser()
            {
                Email = userInfo.Email,
                UserName = userInfo.Email,
                LastName = userInfo.Lastname,
                FirstName = userInfo.Firstname,
                //PasswordHash = userInfo.PassHash
            };
            return await _userManager.CreateAsync(user, userInfo.PassHash);
        }

        private async Task<StudentLoungeUser> FindUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
    }

}