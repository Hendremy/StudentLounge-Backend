using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StudentLounge_Backend.Models.Agendas
{
    public class AgendaEvent
    {
        [Key]
        public string Id { get; set; }
        [JsonIgnore]
        public Agenda Agenda { get; set; }

        public string Label { get; set; }
        public string GroupLabel { get; set; }
        public DateTime StartHour { get; set; }
        public DateTime EndHour { get; set; }
        public DateTime Date => StartHour.Date;

        public AgendaEvent()
        {

        }

        public AgendaEvent(string uid, string label, string groupLabel, DateTime startHour, DateTime endHour)
        {
            Id = uid;
            Label = label;
            GroupLabel = groupLabel;
            StartHour = startHour;
            EndHour = endHour;
        }
    }
}
