using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentLounge_Backend.Models;

namespace StudentLounge_Backend.Controllers
{
    [Route("[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class CountController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public CountController([FromServices] AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("files")]
        public ActionResult<int> GetFileCount()
        {
            try
            {
                int fileCount = _appDbContext.LessonFiles.Count();
                return Ok(fileCount);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("users")]
        public ActionResult<int> GetUserCount()
        {
            try
            {
                int userCount = _appDbContext.Users.Count();
                return Ok(userCount);
            }catch(Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
