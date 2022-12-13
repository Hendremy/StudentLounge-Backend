using System.Text.Json.Serialization;

namespace StudentLounge_Backend.Models.Agendas
{
    public class Agenda
    {
        public int Id { get; set; }
        [JsonIgnore]
        public AppUser User { get; set; }
        public string Name { get; set; }
        public IList<AgendaEvent> AgendaEvents { get; set; }

        public Agenda()
        {

        }

        public Agenda (string name, IEnumerable<AgendaEvent> agendaEvents)
        {
            Name = name;
            AgendaEvents = new List<AgendaEvent>(agendaEvents);
        }
    }
}
