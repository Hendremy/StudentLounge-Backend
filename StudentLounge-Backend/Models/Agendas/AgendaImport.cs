using StudentLounge_Backend.Validations.Agendas;
using System.ComponentModel.DataAnnotations;

namespace StudentLounge_Backend.Models.Agendas
{
    [FileIsICalendar(ErrorMessage ="Invalid file format. Expected iCalendar file.")]
    public class AgendaImport
    {
        [Required]
        public IFormFile CalendarFile { get; set; }
    }
}
