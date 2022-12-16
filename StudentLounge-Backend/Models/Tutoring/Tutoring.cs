using StudentLounge_Backend.Models.Agendas;
using StudentLounge_Backend.Models.Appointments;
using StudentLounge_Backend.Models.Files;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StudentLounge_Backend.Models.Tutorats
{
    public class Tutoring
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public string? TutorId { get; set; }
        public virtual AppUser? Tutor { get; set; }
        public string TutoredId { get; set; }
        public virtual AppUser Tutored { get; set; }

        public virtual Lesson Lesson { get; set; }

        [JsonIgnore]
        public virtual IList<Appointment> Appointments { get; set; }

        public Tutoring() { }

        public Tutoring(AppUser tutored, Lesson lesson)
        {
            Date = DateTime.Now;
            Tutor = null;
            Tutored = tutored;
            Lesson = lesson;
        }
    }
}
