using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentLounge_Backend.Models;
using StudentLounge_Backend.Models.Agendas;
using StudentLounge_Backend.Models.Appointments;
using StudentLounge_Backend.Models.DTOs;

namespace StudentLounge_Backend.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles="Student")]
    [ApiController]
    public class AppointmentsController : SecuredController
    {

        private readonly AppDbContext _appDbContext;

        public AppointmentsController([FromServices] AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public ActionResult<AppointmentDTO> MakeAppointment(AppointmentRequest request)
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
                return Ok(new AppointmentDTO(appointment));
            }
            return ValidationProblem(ModelState);
        }

        [HttpGet]
        public ActionResult<IEnumerable<AppointmentDTO>> GetUserAppointments()
        {
            var user = _appDbContext.AppUsers
                .Find(GetUserId());
            return Ok(user.Appointments.Select(a => new AppointmentDTO(a)));
        }
    }
}
