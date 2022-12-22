using StudentLounge_Backend.Models.Utils;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StudentLounge_Backend.Models.Agendas
{
    public class AgendaEvent
    {
        [Key]
        public int Id { get; set; }
        public string EventId { get; set; }
        [JsonIgnore]
        public virtual Agenda Agenda { get; set; }

        public string Description { get; set; }
        public string? Location { get; set; }
        public string Summary { get; set; }
        public string StartHour { get; set; }
        public string EndHour { get; set; }
        public DateTime Date => DateTime.Parse(StartHour).Date;

        public AgendaEvent()
        {

        }

        public AgendaEvent(string eventId, string description, string location, string summary, DateTime startHour, DateTime endHour)
        {
            EventId = eventId;
            Description = description ?? "";
            Location = location ?? "";
            Summary = summary ?? "";
            StartHour = DateUtils.ToUtcString(startHour);
            EndHour = DateUtils.ToUtcString(endHour);
        }
    }
}
