using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentLounge_Backend.Models;
using StudentLounge_Backend.Models.Agendas;

namespace StudentLounge_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AppointmentController : SecuredController
    {

        private readonly AppDbContext _appDbContext;

        public AppointmentController([FromServices] AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPut]
        public async Task<ActionResult> MakeAppointment(AppointmentRequest request)
        {
            if (ModelState.IsValid)
            {
                var tutoring = _appDbContext.Tutorings.Find(request.TutoringId);
                var start = DateTime.Parse(request.Start);
                var end = DateTime.Parse(request.End);
                var appointment = new Appointment(start, end, request.Location, tutoring);
                _appDbContext.Appointments.Add(appointment);
                tutoring.Appointments.Add(appointment);
                _appDbContext.SaveChanges();
                return Ok(appointment);
            }
            return ValidationProblem(ModelState);
        }

        [HttpGet]
        public async Task<ActionResult> GetUserAppointments()
        {
            var user = _appDbContext.AppUsers.Find(GetUserId());
            return Ok(user.Appointments);
        }
    }
}
