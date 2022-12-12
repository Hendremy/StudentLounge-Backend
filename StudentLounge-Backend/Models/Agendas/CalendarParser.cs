using Ical.Net;
using Ical.Net.CalendarComponents;

namespace StudentLounge_Backend.Models.Agendas
{
    public class CalendarParser : IParseCalendar
    {
        public CalendarCollection ParseFile(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                return CalendarCollection.Load(stream);
            }
        }
    }
}
