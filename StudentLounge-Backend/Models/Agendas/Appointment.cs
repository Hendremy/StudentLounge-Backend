using Ical.Net.CalendarComponents;

namespace StudentLounge_Backend.Models.Agendas
{
    public class Appointment
    {
        public int Id { get; set; }

        public List<AppUser> Users { get; set; }

    }
}
