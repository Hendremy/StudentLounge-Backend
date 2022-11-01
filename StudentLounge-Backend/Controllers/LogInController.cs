using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentLounge_Backend.Models;

namespace StudentLounge_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        private readonly UserManager<StudentLoungeUser> _userManager;
        private readonly SignInManager<StudentLoungeUser> _signInManager;
        private readonly ICreateToken _jwtTokenCreator;

        public LogInController([FromServices]UserManager<StudentLoungeUser> userManager, SignInManager<StudentLoungeUser> signInManager, ICreateToken jwtTokenCreator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenCreator = jwtTokenCreator;
        }

        [HttpPost("LogIn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            var user = await FindUserAsync(userLogin.Username);
            if (user != null)
            {
                var loginResult = await _signInManager.CheckPasswordSignInAsync(user, userLogin.HashPass, false);
                if (loginResult.Succeeded)
                {
                    return Ok(_jwtTokenCreator.Create(user));
                }
            }
            return NotFound("Login failed");
        }

        private async Task<StudentLoungeUser> FindUserAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }
    }
}
