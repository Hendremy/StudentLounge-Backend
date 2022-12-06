using StudentLounge_Backend.Models.Files;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StudentLounge_Backend.Models.Tutorats
{
    public class Tutorat
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public DateTime Date { get; set; }

        public virtual AppUser? Tutor { get; set; }
        public virtual AppUser Tutored { get; set; }

        public virtual Lesson Lesson { get; set; }

        public Tutorat() { }

        public Tutorat(AppUser tutored, Lesson lesson)
        {
            Date = DateTime.Now;
            Tutor = null;
            Tutored = tutored;
            Lesson = lesson;
        }
    }
}
