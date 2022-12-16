using StudentLounge_Backend.Models;
using StudentLounge_Backend.Models.Agendas;
using System.ComponentModel.DataAnnotations;

namespace StudentLounge_Backend.Validations.Agendas
{
    public class TutoringExists : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null && value is AppointmentRequest)
            {
                var req = (AppointmentRequest)value;
                var dbContext = validationContext.GetService<AppDbContext>();
                if (dbContext is not null && dbContext.Tutorings.Find(req.TutoringId) is null)
                {
                    return new ValidationResult("The specified Tutoring does not exist.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
