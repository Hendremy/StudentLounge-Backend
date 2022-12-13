using Ical.Net;
using Ical.Net.CalendarComponents;

namespace StudentLounge_Backend.Models.Agendas
{
    public class AgendaFactory :  ICreateAgendas
    {

        public IList<Agenda> FromCalendarCollection(CalendarCollection calendars)
        {
            var agendas = new List<Agenda>();
            foreach(var calendar in calendars)
            {
                agendas.Add(AgendaFromCalendar(calendar));
            }
            return agendas;
        }

        private Agenda AgendaFromCalendar(Calendar calendar)
        {
            var agendaEvents = new List<AgendaEvent>();
            foreach (var calendarEvent in calendar.Events)
            {
                agendaEvents.Add(AgendaEventFromCalendarEvent(calendarEvent, calendar.Name));
            }
            return new Agenda(agendaEvents);
        }

        private AgendaEvent AgendaEventFromCalendarEvent(CalendarEvent calEvent, string calendarName)
        {
            return new AgendaEvent(calEvent.Uid, calEvent.Description, calendarName , calEvent.Start.AsUtc, calEvent.End.AsUtc);
        }
    }
}
