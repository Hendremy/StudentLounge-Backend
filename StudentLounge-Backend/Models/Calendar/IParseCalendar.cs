﻿using Ical.Net;

namespace StudentLounge_Backend.Models.Calendar
{
    public interface IParseCalendar
    {
        public CalendarCollection ParseFromStream(Stream stream);
    }
}