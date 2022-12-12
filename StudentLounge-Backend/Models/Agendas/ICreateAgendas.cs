using Ical.Net;

namespace StudentLounge_Backend.Models.Agendas
{
    public interface ICreateAgendas
    {
        public IList<Agenda> FromCalendarCollection(CalendarCollection calendarCollection);
    }
}
