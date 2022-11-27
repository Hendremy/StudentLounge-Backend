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
    public class AuthController : ControllerBase
    {
        private readonly IHandleAuth _authHandler;
        private readonly IHandleExternalAuth _externalAuthHandler;
        private readonly ICreateToken _jwtTokenCreator;
        private readonly ICreateUserInfo _userInfoCreator;
        private readonly IHandleUsers _userRepository;

        public AuthController([FromServices]IHandleAuth authHandler, IHandleExternalAuth externalAuthHandler,
            ICreateToken jwtTokenCreator, ICreateUserInfo userInfoCreator, IHandleUsers userRepository)
        {
            _authHandler = authHandler;
            _externalAuthHandler = externalAuthHandler;
            _jwtTokenCreator = jwtTokenCreator;
            _userInfoCreator = userInfoCreator;
            _userRepository = userRepository;
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //TODO: ModelState validation pour renvoyer ValidationProblem
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            var user = await _authHandler.Login(userLogin);
            return user != null ? Ok(await BuildUserInfo(user)) : NotFound();
        }

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //TODO: ModelState validation pour renvoyer ValidationProblem
        public async Task<IActionResult> Register([FromBody] UserRegister userRegister)
        {
            var user = await _authHandler.Register(userRegister);
            return user != null ? Ok(await BuildUserInfo(user)) : BadRequest();
        }

        [HttpPost("External")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Authenticate([FromBody] ExternalAuthRequest request)
        {
            var user = await _externalAuthHandler.HandleAsync(request);
            return user != null ? Ok(await BuildUserInfo(user)) : BadRequest();
        }
        
        private async Task<UserInfo> BuildUserInfo(AppUser user)
        {
            var roles = await _userRepository.GetUserRoles(user);
            return _userInfoCreator.Create(_jwtTokenCreator, user, roles);
        }
    }

}