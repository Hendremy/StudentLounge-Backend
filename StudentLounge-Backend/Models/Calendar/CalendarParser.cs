using Ical.Net;

namespace StudentLounge_Backend.Models.Calendar
{
    public class CalendarParser : IParseCalendar
    {
        public CalendarCollection ParseFromStream(Stream textStream)
        {
            string calendarText = ParseAllText(textStream);
            return CalendarCollection.Load(calendarText);
        }

        private string ParseAllText(Stream stream)
        {
            return "";
        }
    }
}
