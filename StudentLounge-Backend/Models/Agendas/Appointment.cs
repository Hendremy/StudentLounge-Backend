using Ical.Net.CalendarComponents;
using StudentLounge_Backend.Models.Tutorats;
using System.Text.Json.Serialization;

namespace StudentLounge_Backend.Models.Agendas
{
    
    public class Appointment
    {
        public int Id { get; set; }

        public virtual Tutoring Tutoring { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Location { get; set; }

        public Appointment()
        {

        }

        public Appointment(DateTime start, DateTime end, string location, Tutoring tutoring)
        {
            Start = start;
            End = end;
            Location = location;
            Tutoring = tutoring;
        }
    }
}
