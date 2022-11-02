using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentLounge_Backend.Models;

namespace StudentLounge_Backend.Controllers
{
    [Route("[controller]")]
    [AllowAnonymous]
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //TODO: ModelState validation pour renvoyer ValidationProblem
        public async Task<IActionResult> Register([FromBody] UserRegister userRegister)
        {
            var existingUser = await FindUserByEmailAsync(userRegister.Email);
            if (userRegister.Password == userRegister.PasswordRep && existingUser == null) {
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

        [HttpPost("Google")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Google()
        {
            //TODO: Inscription google
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
            return await _userManager.CreateAsync(user, userInfo.Password);
        }

        private async Task<StudentLoungeUser> FindUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
    }

}