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
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ICreateToken _jwtTokenCreator;

        public AuthController([FromServices]UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ICreateToken jwtTokenCreator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenCreator = jwtTokenCreator;
        }

        [HttpPost("Register")]
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

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            var user = await _userManager.FindByNameAsync(userLogin.Username);
            if (user != null)
            {
                var loginResult = await _signInManager.CheckPasswordSignInAsync(user, userLogin.Password, false);
                if (loginResult.Succeeded)
                {
                    return Ok(_jwtTokenCreator.Create(user));
                }
            }
            return NotFound("Login failed");
        }

        private async Task<IdentityResult> CreateUserAsync(UserRegister userInfo)
        {
            var user = new AppUser()
            {
                Email = userInfo.Email,
                UserName = userInfo.Email,
                LastName = userInfo.Lastname,
                FirstName = userInfo.Firstname
            };
            return await _userManager.CreateAsync(user, userInfo.Password);
        }

        private async Task<AppUser> FindUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
    }

}