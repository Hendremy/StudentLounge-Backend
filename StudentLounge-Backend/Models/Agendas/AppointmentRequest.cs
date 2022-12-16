using StudentLounge_Backend.Validations.Agendas;
using System.ComponentModel.DataAnnotations;

namespace StudentLounge_Backend.Models.Agendas
{

    [ValidAppointmentDates(ErrorMessage = "Invalid start and end dates.")]
    [InvitedUserExists]
    public class AppointmentRequest
    {
        [Required]
        public string InvitedId { get; set; }
        [Required]
        public string Start { get; set; }
        [Required]
        public string End { get; set; }
        [Required]
        public string Location { get; set; }
    }
}
