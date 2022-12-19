using StudentLounge_Backend.Models.Appointments;

namespace StudentLounge_Backend.Models.DTOs
{
    public class AppointmentDTO
    {
        public int Id { get; set; }
        public ValidatedTutoringDTO Tutoring { get; set; }
        public string Start { get; set; }
        public string End { get; set; }

        public string Location { get; set; }

        public AppointmentDTO(Appointment appointment)
        {
            Id = appointment.Id;
            Start = appointment.Start;
            End = appointment.End;
            Tutoring = new ValidatedTutoringDTO(appointment.Tutoring);
            Location = appointment.Location;
        }
    }
}
