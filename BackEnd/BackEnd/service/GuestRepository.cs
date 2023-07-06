using AutoMapper;
using BackEnd.Data;
using BackEnd.Dtos;
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
        private readonly IMapper _mapper;
        public GuestRepository(ApplicationDbContext context , 
                               IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public IEnumerable<GuestDtos> GetGuestWithPhoneNumber(DateTime dateFrom, DateTime dateTo, string phoneNumber)
        {
            var listOfGuests = _context.Guests.Where(c => c.DateOfBooking >= dateFrom && 
                                                     c.DateOfBooking <= dateTo && 
                                                     c.Phone == phoneNumber).ToList();
            var guestDtos = _mapper.Map<List<GuestDtos>>(listOfGuests);
            return guestDtos;
        }
        public IEnumerable<GuestDtos> GetGuestWithName(DateTime dateFrom, DateTime dateTo, string Name)
        {
            var listOfGuests = _context.Guests.Where(c => c.DateOfBooking >= dateFrom &&
                                                     c.DateOfBooking <= dateTo &&
                                                     c.Name.ToLower() == Name.ToLower()).ToList();
            var guestDtos = _mapper.Map<List<GuestDtos>>(listOfGuests);
            return guestDtos;
        }

        public IEnumerable<Guest> GetListOfGuests()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UndoCancelGuest(int id , DateTime dateOfBooking)
        {
            var guestInDb = _context.Guests.FirstOrDefault(c => c.Id == id);
            if (guestInDb != null)
            {
                guestInDb.IsCanceled = false;
                guestInDb.DateOfBooking = dateOfBooking;
                _context.Guests.Update(guestInDb);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public IEnumerable<GuestDtos> GetGuestWithName(string Name)
        {
            var listOfGuests = _context.Guests.Where(c => 
                                                     c.Name.ToLower() == Name.ToLower()).ToList();
            var guestDtos = _mapper.Map<List<GuestDtos>>(listOfGuests);
            return guestDtos;
        }

        public IEnumerable<GuestDtos> GetGuestWithPhoneNumber(string phoneNumber)
        {
            var listOfGuests = _context.Guests.Where(c =>
                                                    c.Phone == phoneNumber).ToList();
            var guestDtos = _mapper.Map<List<GuestDtos>>(listOfGuests);
            return guestDtos;
        }

        public GuestDtos getGuestByPhone(string phone)
        {
            var guest = _context.Guests.OrderByDescending(c => c.DateOfBooking).FirstOrDefault(c => c.Phone.ToLower() == phone.ToLower());
            var guestDtos = _mapper.Map<GuestDtos>(guest);
            return guestDtos;
        }
        public GuestDtos getGuestByName(string name)
        {
            var guest = _context.Guests.OrderByDescending(c => c.DateOfBooking).FirstOrDefault(c => c.Name.ToLower() == name.ToLower());
            var guestDtos = _mapper.Map<GuestDtos>(guest);
            return guestDtos;
        }
    }
}
