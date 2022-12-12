using Ical.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentLounge_Backend.Models;
using StudentLounge_Backend.Models.Calendar;

namespace StudentLounge_Backend.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles="Student")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly IParseCalendar _calendarParser;
        private readonly AppDbContext _appDbContext;

        public CalendarController([FromServices]IParseCalendar calendarParser, AppDbContext appDbContext)
        {
            _calendarParser = calendarParser;
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<ActionResult> ImportCalendar(CalendarImport import)
        {
            try
            {
                var calendar = import.CalendarFile;
                if (calendar.FileName.EndsWith(".ics"))
                {
                    CalendarCollection? calendars = null;
                    using (var stream = calendar.OpenReadStream())
                    {
                        calendars = _calendarParser.ParseFile(calendar);

                    }
                    return Ok(calendars);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetSchedule()
        {
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> MakeAppointment()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetAppointments()
        {
            return Ok();
        }
    }
}
