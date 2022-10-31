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
        private readonly IConfiguration _config;
        private readonly UserManager<StudentLoungeUser> _userManager;
        private readonly IJwtTokenCreator _jwtTokenCreator;

        public RegisterController(IConfiguration config, [FromServices]UserManager<StudentLoungeUser> userManager, IJwtTokenCreator jwtTokenCreator)
        {
            _userManager = userManager;
            _config = config;
            _jwtTokenCreator = jwtTokenCreator;
        }

        [HttpPost("PostUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignUp(UserInfo userInfo)
        {
            var exists = await GetUserByEmailAsync(userInfo.Email);
            if (userInfo.Pass == userInfo.PassRep && exists != null) {
                var result = await CreateUserAsync(userInfo);
                if (result.Succeeded)
                {
                    var account = await GetUserByEmailAsync(userInfo.Email);
                    return Ok(_jwtTokenCreator.CreateToken(account));
                }
                else
                {
                    return Problem();
                }
            }
            return BadRequest();
        }


        private async Task<IdentityResult> CreateUserAsync(UserInfo userInfo)
        {
            var user = new StudentLoungeUser()
            {
                Email = userInfo.Email,
                UserName = userInfo.Email,
                LastName = userInfo.Lastname,
                FirstName = userInfo.Firstname
            };
            return await _userManager.CreateAsync(user, userInfo.Pass);
        }

        private async Task<StudentLoungeUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email); ;
        }
    }

}