using BackEnd.Data;
using BackEnd.Iservices;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.service
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _context;

        public TicketRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Ticket> AddTicket(Ticket model)
        {
            var result = _context.tickets.Add(model);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Ticket> DeleteTicket(int id)
        {
            var guestTickets = _context.GuestTickets.FirstOrDefault(c => c.TicketId == id);
            var ticket = _context.tickets.FirstOrDefault(c => c.Id == id);
            if (guestTickets != null)
            {
                ticket.IsDeleted = true;
                _context.tickets.Update(ticket);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.tickets.Remove(ticket);
                await _context.SaveChangesAsync();
            }
            return ticket;
        }

        public async Task<Ticket> EditTicket(Ticket model)
        {
            var ticket = _context.tickets.FirstOrDefault(c => c.Id == model.Id);
            if (ticket != null)
            {
                ticket.TicketName = model.TicketName;
                ticket.PriceForAdult = model.PriceForAdult;
                ticket.PriceForKids = model.PriceForKids;
                ticket.Sorting = model.Sorting;
                _context.tickets.Update(ticket);
                await _context.SaveChangesAsync();
                return ticket;
            }
            return null;
        }

        public List<Ticket> GetListOfTickets()
        {
            return _context.tickets.OrderBy(c => c.Sorting).ToList();
        }

        public async Task<Ticket> GetTicketById(int id)
        {
            var ticket = _context.tickets.FirstOrDefaultAsync(c => c.Id == id);
            return await ticket;
        }

        public async Task<Ticket> UndoDeleteTicket(int id)
        {
            var ticket = _context.tickets.FirstOrDefault(c => c.Id == id);
            if (ticket != null)
            {
                ticket.IsDeleted = false;
                _context.tickets.Update(ticket);
                await _context.SaveChangesAsync();
            }
            return ticket;
        }
    }
}
