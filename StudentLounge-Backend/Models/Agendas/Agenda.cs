namespace StudentLounge_Backend.Models.Agendas
{
    public class Agenda
    {
        public int Id { get; set; }
        public IList<AgendaEvent> AgendaEvents { get; set; }
        //public IList<Appointment> Appointments { get; set; }
    }
}
