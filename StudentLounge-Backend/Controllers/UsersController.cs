using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentLounge_Backend.Models;
using StudentLounge_Backend.Models.Authentication;
using StudentLounge_Backend.Models.DTOs;
using StudentLounge_Backend.Models.Files;

namespace StudentLounge_Backend.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class UsersController : SecuredController
    {
        private readonly AppDbContext _appDbContext;
        private readonly IHandleUsers _userRepository;

        public UsersController(AppDbContext appDbContext, IHandleUsers userRepository)
        {
            _appDbContext = appDbContext;
            _userRepository = userRepository;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var users = _appDbContext.AppUsers.ToList();
            return Ok(ConvertUserToDTO(users));
        }

        [HttpPost("update/{userId}")]
        public async Task<ActionResult> UpdateUser(string userId, string? firstname, string? lastname, string? username, string? password)
        {
            var user = _appDbContext.AppUsers.FirstOrDefault(user => user.Id == userId);
            if(user != null)
            {
                if(username != null)
                {
                    user.UserName = username;
                }
                if (firstname != null)
                {
                    user.Firstname = firstname;
                }
                if(lastname != null)
                {
                    user.Lastname= lastname;
                }

                await _userRepository.UpdateUser(user, password);
                _appDbContext.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest("Bad Id");
            }
        }

        [HttpDelete("delete/{userId}")]
        public async Task<ActionResult> DeleteUser(string userId)
        {
            var user = _appDbContext.AppUsers.FirstOrDefault(user => user.Id == userId);
            if(user != null)
            {
                _appDbContext.AppUsers.Remove(user);
                _appDbContext.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        private IEnumerable<UserDTO> ConvertUserToDTO(IEnumerable<AppUser> users)
        {
            IList<UserDTO> usersDTO = new List<UserDTO>();
            foreach (var user in users)
            {
                usersDTO.Add(new UserDTO(user));
            }
            return usersDTO;
        }

    }
}
