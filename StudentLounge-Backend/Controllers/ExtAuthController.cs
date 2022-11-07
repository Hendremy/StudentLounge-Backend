using Google.Apis.Auth;
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
    public class ExtAuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ICreateToken _jwtTokenCreator;

        public ExtAuthController([FromServices]UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ICreateToken jwtTokenCreator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenCreator = jwtTokenCreator;
        }

        [HttpPost("{providerName:string}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Google(string providerName, string jwt)
        {
            try {
                var payload = await GoogleJsonWebSignature.ValidateAsync(jwt);
                var userId = payload.Subject;

                var user = await _userManager.FindByLoginAsync("Google", userId);
                if (user == null)
                {

                }
                var token = _jwtTokenCreator.Create(user);
                return Ok(token);
            }catch(InvalidJwtException ex)
            {
                return Unauthorized();
            }
        }
    }
}
