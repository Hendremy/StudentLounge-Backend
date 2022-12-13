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
                agendaEvents.Add(AgendaEventFromCalendarEvent(calendarEvent));
            }
            return new Agenda(calendar.Name,agendaEvents);
        }

        private AgendaEvent AgendaEventFromCalendarEvent(CalendarEvent calEvent)
        {
            return new AgendaEvent(
                id: calEvent.Uid, 
                description: calEvent.Description,
                summary: calEvent.Summary,
                location: calEvent.Location,
                startHour: calEvent.Start.AsUtc, 
                endHour: calEvent.End.AsUtc);
        }
    }
}
