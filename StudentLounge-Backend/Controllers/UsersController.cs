using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentLounge_Backend.Models;
using StudentLounge_Backend.Models.DTOs;
using StudentLounge_Backend.Models.Files;

namespace StudentLounge_Backend.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class UsersController : SecuredController
    {
        private readonly AppDbContext _appDbContext;

        public UsersController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var users = _appDbContext.AppUsers.ToList();
            return Ok(ConvertUserToDTO(users));
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
