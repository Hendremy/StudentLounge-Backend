using Ical.Net;

namespace StudentLounge_Backend.Models.Agendas
{
    public interface IParseCalendar
    {
        public CalendarCollection ParseFile(IFormFile file);
    }
}
