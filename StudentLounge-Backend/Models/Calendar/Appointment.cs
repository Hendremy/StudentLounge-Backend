using Ical.Net.CalendarComponents;

namespace StudentLounge_Backend.Models.Calendar
{
    public class Appointment
    {

        public CalendarEvent Details { get; set; }

        public List<AppUser> Users { get; set; }

    }
}
