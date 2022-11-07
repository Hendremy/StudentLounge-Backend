using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentLounge_Backend.Models;
using StudentLounge_Backend.Models.Authentication;

namespace StudentLounge_Backend.Controllers
{
    [Route("[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class ExtAuthController : ControllerBase
    {
        private readonly IHandleExternalAuth _externalAuthHandler;
        private readonly ICreateToken _jwtTokenCreator;

        public ExtAuthController([FromServices]IHandleExternalAuth externalAuthHandler, ICreateToken jwtTokenCreator)
        {
            _externalAuthHandler = externalAuthHandler;
            _jwtTokenCreator = jwtTokenCreator;
        }

        [HttpPost("{providerName:string}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Authenticate(string providerName, string jwt)
        {

        }
    }
}
