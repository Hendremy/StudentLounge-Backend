using StudentLounge_Backend.Models.Files;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StudentLounge_Backend.Models.Tutorats
{
    public class Tutorat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public DateTime Date { get; set; }

        public AppUser? Tutor { get; set; }
        public AppUser Tutored { get; set; }

        public Lesson Lesson { get; set; }
    }
}
