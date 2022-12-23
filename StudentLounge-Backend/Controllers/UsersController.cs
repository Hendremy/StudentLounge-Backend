using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentLounge_Backend.Models;
using StudentLounge_Backend.Models.Authentication;
using StudentLounge_Backend.Models.DTOs;
using StudentLounge_Backend.Models.Files;
using StudentLounge_Backend.Models.UpdateUsers;

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

        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetUsers()
        {
            var users = _appDbContext.AppUsers.ToList();
            return Ok(ConvertUserToDTO(users));
        }

        [HttpPut("{userId}")]
        public async Task<ActionResult> UpdateUser(string userId, [FromBody] UserUpdated userUpdated)
        {
            var user = _appDbContext.AppUsers.FirstOrDefault(user => user.Id == userId);
            if(user != null)
            {
                if(userUpdated.Username != null)
                {
                    user.UserName = userUpdated.Username;
                }
                if (userUpdated.Firstname != null)
                {
                    user.Firstname = userUpdated.Firstname;
                }
                if(userUpdated.Lastname != null)
                {
                    user.Lastname= userUpdated.Lastname;
                }

                await _userRepository.UpdateUser(user, userUpdated.Password);
                _appDbContext.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest("Bad Id");
            }
        }

        [HttpDelete("{userId}")]
        public ActionResult DeleteUser(string userId)
        {
            var user = _appDbContext.AppUsers.FirstOrDefault(user => user.Id == userId);
            if (user != null)
            {
                var tutorings = _appDbContext.Tutorings.Where(tutoring => tutoring.TutorId == userId);
                _appDbContext.Tutorings.RemoveRange(tutorings);
                _appDbContext.AppUsers.Remove(user);
                _appDbContext.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("{userId}")]
        public async Task<ActionResult> BlockUser(string userId)
        {
            var user = _appDbContext.AppUsers.FirstOrDefault(user => user.Id == userId);
            if (user != null)
            {
                var result = await _userRepository.BlockUser(user);
                _appDbContext.SaveChanges();
                return Ok(result);
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
