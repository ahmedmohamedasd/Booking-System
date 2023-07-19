using BackEnd.Data;
using BackEnd.Iservices;
using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.service
{
    public class GuestTicketRepository : IGuestTicketRepository
    {
        private readonly ApplicationDbContext _context;
        public GuestTicketRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<List<GuestTicket>> AddGuestTicket(List<GuestTicket> model)
        {
            for(int i = 0; i < model.Count; i++)
            {
                _context.GuestTickets.Add(model[i]);
                await _context.SaveChangesAsync();
            }
            return model;
        }

        public async Task<GuestTicket> DeleteGuestTickets(int guestId, int ticketId)
        {
            var guestTicketInDb = _context.GuestTickets.FirstOrDefault(c => c.GuestId == guestId && c.TicketId == ticketId);
            if(guestTicketInDb != null)
            {
                _context.GuestTickets.Remove(guestTicketInDb);
                await _context.SaveChangesAsync();
            }
            return guestTicketInDb;
        }

        public async Task<List<GuestTicket>> DeleteGuestTickets(int guestId)
        {
            var guestTicketInDb = _context.GuestTickets.Where(c => c.GuestId == guestId).ToList();
            if (guestTicketInDb.Count >0)
            {
                for(int i = 0; i < guestTicketInDb.Count; i++)
                {
                    _context.GuestTickets.Remove(guestTicketInDb[i]);
                    await _context.SaveChangesAsync();
                }
            }
            return guestTicketInDb;
        }
        public async Task<GuestTicket> EditGuestTickets(int guestId, GuestTicket model)
        {
            var guestTicketInDb = _context.GuestTickets.FirstOrDefault(c => c.GuestId == guestId && c.TicketId == model.TicketId);
            if(guestTicketInDb != null)
            {
                guestTicketInDb.CountOfAdult = model.CountOfAdult;
                guestTicketInDb.CountOfKids = model.CountOfKids;
                _context.GuestTickets.Update(guestTicketInDb);
                await _context.SaveChangesAsync();
            }
            return model;
        }

        public async Task<int> EditGuestTicketsDateBooking(int guestId, DateTime DateOfBooking)
        {
            var model = _context.GuestTickets.Where(c => c.GuestId == guestId).ToList();
            if (model.Count > 0)
            {
                for (int i = 0; i < model.Count; i++)
                {
                    model[i].DateOfBooking = DateOfBooking;
                    _context.GuestTickets.Update(model[i]);
                    await _context.SaveChangesAsync();
                }
                return guestId;
            }

            return guestId;
        }

        public List<GuestTicket> GetListOfGuestTickets(int guestId)
        {
            return _context.GuestTickets.Where(c=>c.GuestId == guestId).ToList();
        }
    }
}
