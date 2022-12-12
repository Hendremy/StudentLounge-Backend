using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudentLounge_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        [HttpPut]
        public async Task<ActionResult> MakeAppointment()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetUserAppointments()
        {
            return Ok();
        }
    }
}
