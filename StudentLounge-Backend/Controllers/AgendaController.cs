using Ical.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentLounge_Backend.Models;
using StudentLounge_Backend.Models.Agendas;

namespace StudentLounge_Backend.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles="Student")]
    [ApiController]
    public class AgendaController : SecuredController
    {
        private readonly IParseCalendar _calendarParser;
        private readonly ICreateAgendas _createAgendas;
        private readonly AppDbContext _appDbContext;

        public AgendaController([FromServices] AppDbContext appDbContext, IParseCalendar calendarParser, ICreateAgendas createAgendas)
        {
            _calendarParser = calendarParser;
            _appDbContext = appDbContext;
            _createAgendas = createAgendas;
        }

        [HttpPost]
        public async Task<ActionResult> ImportCalendar([FromForm] AgendaImport import)
        {
            try
            {
                var calendarFile = import.CalendarFile;
                if (calendarFile.FileName.EndsWith(".ics"))
                {
                    var user = _appDbContext.AppUsers
                        .Include(user => user.Agendas)
                        .FirstOrDefault(u => u.Id == GetUserId());
                    if(user != null)
                    {
                        var calendars = _calendarParser.ParseFile(calendarFile);
                        user.Agendas = _createAgendas.FromCalendarCollection(calendars);
                        _appDbContext.Update(user);
                        _appDbContext.SaveChanges();
                        return Ok(user.Agendas);
                    }
                }
                return BadRequest("Invalid file format");
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetUserAgendas()
        {
            var user = _appDbContext.AppUsers
                .Include(user => user.Agendas)
                .ThenInclude(agendas => agendas.AgendaEvents)
                .FirstOrDefault(u => u.Id == GetUserId());
            if(user != null)
            {
                return Ok(user.Agendas);
            }
            return BadRequest();
        }
    }
}
