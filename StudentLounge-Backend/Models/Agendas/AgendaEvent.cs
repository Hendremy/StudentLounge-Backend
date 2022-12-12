using System.ComponentModel.DataAnnotations;

namespace StudentLounge_Backend.Models.Agendas
{
    public class AgendaEvent
    {
        [Key]
        public string Id { get; set; }

        public string Label { get; set; }
        public string? Color { get; set; }
        public string GroupLabel { get; set; }
        public DateTime StartHour { get; set; }
        public DateTime EndHour { get; set; }
        public DateTime Date => StartHour.Date;

        public AgendaEvent()
        {

        }

        public AgendaEvent(string uid, string label, string groupLabel, DateTime startHour, DateTime endHour, string? color = "")
        {
            Id = uid;
            Label = label;
            Color = color;
            GroupLabel = groupLabel;
            StartHour = startHour;
            EndHour = endHour;
        }
    }
}
