using Ical.Net;

namespace StudentLounge_Backend.Models.Calendar
{
    public class CalendarParser : IParseCalendar
    {
        public CalendarCollection ParseFile(IFormFile file)
        {
            return CalendarCollection.Load(file.OpenReadStream());
        }
    }
}
