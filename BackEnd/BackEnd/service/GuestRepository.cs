using BackEnd.Data;
using BackEnd.Iservices;
using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.service
{
    public class GuestRepository : IGuestRepository
    {
        private readonly ApplicationDbContext _context;
        
        public GuestRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<Guest> AddGuest(Guest model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CancelGuest(int id)
        {
            var guestInDb = _context.Guests.FirstOrDefault(c => c.Id == id);
            if(guestInDb != null)
            {
                guestInDb.IsCanceled = true;
                _context.Guests.Update(guestInDb);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteGuest(int id)
        {
            var guestInDb = _context.Guests.FirstOrDefault(c => c.Id == id);
            if(guestInDb != null)
            {
                _context.Guests.Remove(guestInDb);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }

        }

        public Task<Guest> EditGuest(Guest model)
        {
            throw new NotImplementedException();
        }

        public Guest GetGuestById(int id)
        {
            var guestInDb = _context.Guests.FirstOrDefault(c => c.Id == id);
            if (guestInDb != null)
            {
                return guestInDb;
            }
            return null;
        }

        public IEnumerable<Guest> GetListOfGuests()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UndoCancelGuest(int id)
        {
            var guestInDb = _context.Guests.FirstOrDefault(c => c.Id == id);
            if (guestInDb != null)
            {
                guestInDb.IsCanceled = false;
                _context.Guests.Update(guestInDb);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
