using StudentLounge_Backend.Models;
using StudentLounge_Backend.Models.Agendas;
using System.ComponentModel.DataAnnotations;

namespace StudentLounge_Backend.Validations.Agendas
{
    public class InvitedUserExists : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null && value is AppointmentRequest)
            {
                var req = (AppointmentRequest)value;
                var dbContext = validationContext.GetService<AppDbContext>();
                if (dbContext is not null && dbContext.AppUsers.Find(req.InvitedId) is null)
                {
                    return new ValidationResult("Invited user does not exist.");
                }
            }
            return base.IsValid(value, validationContext);
        }
    }
}
