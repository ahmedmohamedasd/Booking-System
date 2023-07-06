﻿using BackEnd.Data;
using BackEnd.Iservices;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.service
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IGuestEventRepository _guestEventRepo;
        public EventRepository(ApplicationDbContext context , 
                               IGuestEventRepository guestEventRepository)
        {
            _context = context;
            _guestEventRepo = guestEventRepository;
        }
        public async Task<Event> AddEvent(Event model)
        {
            var result = _context.Events.Add(model);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Event> DeleteEvent(int id)
        {
            var eventInDb = _context.Events.FirstOrDefault(c => c.Id == id);
            if(eventInDb != null)
            {
                _context.Events.Remove(eventInDb);
                await _context.SaveChangesAsync();
            }
            return eventInDb;
        }

        public async Task<Event> EditEvent(Event model)
        {
            var events = _context.Events.FirstOrDefault(c => c.Id == model.Id);

            if (events != null)
            {
                if(events.DateOfEvent != model.DateOfEvent || events.EventPrice !=model.EventPrice )
                {
                    await _guestEventRepo.EditListOfGuestByDateOfEvent(model.Id, model.DateOfEvent, model.EventPrice);
                }
                
                events.DateOfEvent = model.DateOfEvent;
                events.EventName = model.EventName;
                events.EventPrice = model.EventPrice;

                _context.Events.Update(events);
                await _context.SaveChangesAsync();
                return events;
            }
            return null;
        }

        public async Task<Event> GetEventById(int id)
        {
            var events = _context.Events.FirstOrDefaultAsync(c => c.Id == id);
            return await events;
        }

        public List<Event> GetListOfEvents(DateTime dateOfNow)
        {
            var events = _context.Events.Where(c => c.DateOfEvent >= dateOfNow).ToList();
            return events;
        }
        public List<Event> GetListOfEvents()
        {
            var events = _context.Events.OrderByDescending(c=>c.DateOfEvent).ToList();
            return events;
        }

        public Task<Event> UndoDeleteEvent(int id)
        {
            throw new NotImplementedException();
        }
    }
}
