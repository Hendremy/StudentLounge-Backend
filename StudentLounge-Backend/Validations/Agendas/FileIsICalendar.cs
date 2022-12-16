using StudentLounge_Backend.Models.Agendas;
using System.ComponentModel.DataAnnotations;

namespace StudentLounge_Backend.Validations.Agendas
{
    public class FileIsICalendar : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if(value is not null && value is AgendaImport)
            {
                AgendaImport import = value as AgendaImport;
                var file = import.CalendarFile;
                if(file.ContentType is not "text/calendar")
                {
                    return false;
                }
            }
            return base.IsValid(value);
        }
    }
}
