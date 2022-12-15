using Ical.Net.CalendarComponents;

namespace StudentLounge_Backend.Models.Agendas
{
    
    public class Appointment
    {
        public int Id { get; set; }

        public virtual IList<AppUser> Users { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Location { get; set; }

        public Appointment()
        {

        }

        public Appointment(DateTime start, DateTime end, string location, params AppUser[] users)
        {
            Start = start;
            End = end;
            Location = location;
            Users = new List<AppUser>(users);
        }
    }
}
