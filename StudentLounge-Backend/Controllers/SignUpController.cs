using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentLounge_Backend.Models;

namespace StudentLounge_Backend.Controllers
{
    //!!! Attention, avec Identity normal, serveur ne devient plus stateless, utiliser JWT
    [Route("[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly UserManager<StudentLoungeUser> _userManager;
        public SignUpController([FromServices]UserManager<StudentLoungeUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("PostUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignUp(UserInfo userInfo)
        {
            if (userInfo.Pass == userInfo.PassRep) {
                var user = new StudentLoungeUser()
                {
                    Email = userInfo.Email,
                    UserName = userInfo.Email,
                    LastName = userInfo.Lastname,
                    FirstName = userInfo.Firstname
                };
                await _userManager.CreateAsync(user, userInfo.Pass);
                return Ok();
            }
            return BadRequest();
        }
    }

}