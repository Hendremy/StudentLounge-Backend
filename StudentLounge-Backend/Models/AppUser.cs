using Ical.Net;
using Microsoft.AspNetCore.Identity;
using NuGet.Packaging;
using StudentLounge_Backend.Models.Agendas;
using StudentLounge_Backend.Models.Appointments;
using StudentLounge_Backend.Models.Files;
using StudentLounge_Backend.Models.Tutorats;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StudentLounge_Backend.Models
{
    public class AppUser : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public string? Image { get; set; }

        public string Fullname => $"{Firstname} {Lastname}";

        public virtual IList<Agenda> Agendas { get; set; } = new List<Agenda>();

        [JsonIgnore]
        public virtual List<Lesson> Lessons { get; set; } = new List<Lesson>();

        [JsonIgnore]
        public virtual List<LessonFile> PostedFiles { get; set; } = new List<LessonFile>();

        [JsonIgnore]
        public virtual ICollection<Tutoring> AcceptedTutorings { get; set; } = new List<Tutoring>();

        [JsonIgnore]
        public virtual ICollection<Tutoring> TutoringRequests { get; set; } = new List<Tutoring>();

        [NotMapped]
        [JsonIgnore]
        public virtual IList<Tutoring> AllTutorings
        {
            get
            {
                var tutorings = new List<Tutoring>(AcceptedTutorings);
                tutorings.AddRange(TutoringRequests);
                return tutorings;
            }
        }

        [NotMapped]
        [JsonIgnore]
        public virtual IList<Appointment> Appointments
        {
            get
            {
                IList<Appointment> appointments = new List<Appointment>();
                foreach(var tutoring in AllTutorings)
                {
                    appointments.AddRange(tutoring.Appointments);
                }
                return appointments;
            }
        }
    }
}
