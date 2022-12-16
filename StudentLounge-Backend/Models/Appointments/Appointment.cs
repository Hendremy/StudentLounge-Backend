using Ical.Net.CalendarComponents;
using StudentLounge_Backend.Models.Tutorats;
using StudentLounge_Backend.Models.Utils;
using System.Text.Json.Serialization;

namespace StudentLounge_Backend.Models.Appointments
{
    
    public class Appointment
    {
        public int Id { get; set; }

        public virtual Tutoring Tutoring { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Location { get; set; }

        public Appointment()
        {

        }

        public Appointment(DateTime start, DateTime end, string location, Tutoring tutoring)
        {
            Start = DateUtils.ToUtcString(start);
            End = DateUtils.ToUtcString(end);
            Location = location;
            Tutoring = tutoring;
        }
    }
}
