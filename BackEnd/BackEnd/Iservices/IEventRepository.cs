﻿using BackEnd.Dtos;
using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Iservices
{
    public interface IEventRepository
    {
        Task<Event> AddEvent(Event model);
        Task<Event> EditEvent(Event model);
        Task<Event> GetEventById(int id);
        Task<Event> DeleteEvent(int id);
        Task<Event> UndoDeleteEvent(int id); 
        List<EventDtos> GetListOfEvents(DateTime dateOfNow);
        List<EventDtos> GetListOfEventsWithDate(DateTime dateOfNow);

        List<Event> GetListOfEvents();


    }
}
