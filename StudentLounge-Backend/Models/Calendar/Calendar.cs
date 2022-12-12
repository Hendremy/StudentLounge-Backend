using Ical.Net;
using Ical.Net.CalendarComponents;

namespace StudentLounge_Backend.Models.Calendar
{
    public class Calendar
    {
        public AppUser User { get; set; }
        public CalendarCollection Calendars { get; set; }
        
    }
}
