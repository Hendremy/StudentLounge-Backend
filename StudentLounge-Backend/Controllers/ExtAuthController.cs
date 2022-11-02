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
        private readonly UserManager<StudentLoungeUser> _userManager;
        private readonly SignInManager<StudentLoungeUser> _signInManager;
        private readonly ICreateToken _jwtTokenCreator;

        public ExtAuthController([FromServices]UserManager<StudentLoungeUser> userManager, SignInManager<StudentLoungeUser> signInManager, ICreateToken jwtTokenCreator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenCreator = jwtTokenCreator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login(string provider, string providerKey)
        {
            var loginSupported = await ProviderSupportedAsync(provider);
            if (loginSupported) {
                var user = await _userManager.FindByLoginAsync(provider, providerKey);
                if (user != null)
                {
                    return Ok(_jwtTokenCreator.Create(user));
                }
                else 
                {
                    user = new StudentLoungeUser()
                    {
                        //TODO: Créer l'utilisateur sur base de son compte externe
                    };
                }
            }
            return BadRequest("Login provider not supported");
        }

        private async Task<bool> ProviderSupportedAsync(string providerName)
        {
            var providers = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return providers.Any(p => p.Name == providerName);
        }

        [HttpPost("Apple")]
        public async Task<IActionResult> Apple([FromBody] StudentLoungeUser user) 
        {
            return null;
        }
    }
}
