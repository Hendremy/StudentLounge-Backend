using StudentLounge_Backend.Models.Agendas;
using StudentLounge_Backend.Models.Appointments;
using System.ComponentModel.DataAnnotations;

namespace StudentLounge_Backend.Validations.Agendas
{
    public class ValidAppointmentDates : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if(value is not null && value is Appointment)
            {
                AppointmentRequest appointment = value as AppointmentRequest;
                DateTime start = DateTime.Parse(appointment.Start);
                DateTime end = DateTime.Parse(appointment.End);
                if(start > end)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
