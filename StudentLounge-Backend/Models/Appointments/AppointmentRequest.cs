using StudentLounge_Backend.Validations.Agendas;
using System.ComponentModel.DataAnnotations;

namespace StudentLounge_Backend.Models.Appointments
{

    [ValidAppointmentDates(ErrorMessage = "Invalid start and end dates.")]
    [TutoringExists]
    public class AppointmentRequest
    {
        [Required]
        public string TutoringId { get; set; }
        [Required]
        public string Start { get; set; }
        [Required]
        public string End { get; set; }
        [Required]
        public string Location { get; set; }
    }
}
