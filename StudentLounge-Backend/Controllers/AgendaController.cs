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
        private readonly ILogger<AgendaController> _logger;

        public AgendaController(ILogger<AgendaController> logger, [FromServices] AppDbContext appDbContext, IParseCalendar calendarParser, ICreateAgendas createAgendas)
        {
            _logger = logger;
            _calendarParser = calendarParser;
            _appDbContext = appDbContext;
            _createAgendas = createAgendas;
        }

        [HttpPost]
        public ActionResult ImportCalendar([FromForm]AgendaImport import)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _appDbContext.AppUsers.Find(GetUserId());
                    var calendars = _calendarParser.ParseFile(import.CalendarFile);
                    var agendas = _createAgendas.FromCalendarCollection(calendars);
                    user.Agendas.Clear();
                    user.Agendas = agendas;
                    _appDbContext.SaveChanges();
                    return Ok(user.Agendas);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return StatusCode(500, ex.StackTrace);
            }
        }

        [HttpGet]
        public ActionResult GetUserAgendas()
        {
            var user = _appDbContext.AppUsers.Find(GetUserId());
            if (user != null)
            {
                return Ok(user.Agendas);
            }
            return NotFound("User not found.");
        }
    }
}
