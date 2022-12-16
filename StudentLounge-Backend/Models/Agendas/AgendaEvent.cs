using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StudentLounge_Backend.Models.Agendas
{
    public class AgendaEvent
    {
        [Key]
        public string Id { get; set; }
        [JsonIgnore]
        public virtual Agenda Agenda { get; set; }

        public string Description { get; set; }
        public string Location { get; set; }
        public string Summary { get; set; }
        public string StartHour { get; set; }
        public string EndHour { get; set; }
        public DateTime Date => DateTime.Parse(StartHour).Date;

        public AgendaEvent()
        {

        }

        public AgendaEvent(string id, string description, string location, string summary, DateTime startHour, DateTime endHour)
        {
            Id = id;
            Description = description;
            Location = location;
            Summary = summary;
            StartHour = $"{startHour.ToUniversalTime():O}";
            EndHour = $"{endHour.ToUniversalTime():O}";
        }
    }
}
