using BackEnd.Data;
using BackEnd.Iservices;
using BackEnd.Models;
using BackEnd.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.service
{
    public class GuestEventRepository : IGuestEventRepository
    {
        private readonly ApplicationDbContext _context;
        public GuestEventRepository(ApplicationDbContext context){
            _context = context; 
        }

        public async Task<GuestEvent> AddguestEvent(GuestEvent model)
        {
            _context.GuestEvents.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<GuestEvent> CancelGuestEvent(int id)
        {
            var guestEventInDb = _context.GuestEvents.FirstOrDefault(c => c.Id == id);
            guestEventInDb.IsCanceled = true;
            _context.GuestEvents.Update(guestEventInDb);
            await _context.SaveChangesAsync();
            return guestEventInDb;
        }
        public async Task<GuestEvent> UndoCancelGuestEvent(int id)
        {
            var guestEventInDb = _context.GuestEvents.FirstOrDefault(c => c.Id == id);
            guestEventInDb.IsCanceled = false;
            _context.GuestEvents.Update(guestEventInDb);
            await _context.SaveChangesAsync();
            return guestEventInDb;
        }

        public async Task<GuestEvent> DeleteGuestEvent(int id)
        {
            var guestEventInDb = _context.GuestEvents.FirstOrDefault(c => c.Id == id);
            _context.Remove(guestEventInDb);
            await _context.SaveChangesAsync();
            return guestEventInDb;
        }

        public async Task<GuestEvent> EditGuestevent(GuestEvent model)
        {
            var guestEventinDb = _context.GuestEvents.FirstOrDefault(c => c.Id == model.Id);
            
                guestEventinDb.DateOfDeposit = model.DateOfDeposit;
                guestEventinDb.DateOfEvent = model.DateOfEvent;
                guestEventinDb.Deposit = model.Deposit;
                guestEventinDb.DepositWayId = model.DepositWayId;
                guestEventinDb.DiscountByAmount = model.DiscountByAmount;
                guestEventinDb.DiscountByPercentage = model.DiscountByPercentage;
                guestEventinDb.Email = model.Email;
                guestEventinDb.EventId = model.EventId;
                guestEventinDb.EventPrice = model.EventPrice;
                guestEventinDb.GrandTotal = model.GrandTotal;
                guestEventinDb.Identifier = model.Identifier;
                guestEventinDb.IsCanceled = model.IsCanceled;
                guestEventinDb.KnowUsId = model.KnowUsId;
                guestEventinDb.LeftToPay = model.LeftToPay;
                guestEventinDb.Name = model.Name;
                guestEventinDb.PaymentCash = model.PaymentCash;
                guestEventinDb.PaymentVisa = model.PaymentVisa;
                guestEventinDb.Phone = model.Phone;
                guestEventinDb.TotalCountOfguest = model.TotalCountOfguest;
                guestEventinDb.WhereYouId = model.WhereYouId;
            _context.GuestEvents.Update(guestEventinDb);
            await _context.SaveChangesAsync();
            return model;
           
        }

        public EditGuestEventViewModel GetEditGuestEventViewModel(int id)
        {
            var guestEventInDb = _context.GuestEvents.FirstOrDefault(c => c.Id == id);
            var dateOFNow = DateTime.UtcNow.Date;
            var checkDate = dateOFNow;
            if (dateOFNow >= guestEventInDb.DateOfEvent.Date)
            {
                checkDate = guestEventInDb.DateOfEvent.Date;
            }
            else
            {
                checkDate = dateOFNow;
            }
            var Events = _context.Events.Where(c => c.DateOfEvent.Date >= checkDate).ToList();
            var places = _context.WhereYouFroms.OrderBy(c => c.Sorting).ToList();
            var Socialwayes = _context.HowYouKnowUss.OrderBy(c => c.Sorting).ToList();
            var depositWayes = _context.DepositWays.ToList();
            var model = new EditGuestEventViewModel
            {
                DepositWays = depositWayes,
                Events = Events,
                HowYouKnowUs = Socialwayes,
                WhereYouFrom = places,
                GuestEventInfo = guestEventInDb
            };
            return model;
        }

        public async Task<GuestEvent> GetGuestEventById(int id)
        {
            var guestEventInDb = await _context.GuestEvents.FirstOrDefaultAsync(c => c.Id == id);
            return guestEventInDb;
        }

        public AddGuestEventViewModel GetGuestEventViewModel()
        {
            var dateOFNow = DateTime.UtcNow.Date;
            var places = _context.WhereYouFroms.OrderBy(c=>c.Sorting).ToList();
            var Socialwayes = _context.HowYouKnowUss.OrderBy(c => c.Sorting).ToList();
            var depositWayes = _context.DepositWays.ToList();
            var Events = _context.Events.Where(c => c.DateOfEvent.Date >= dateOFNow).ToList();
            var model = new AddGuestEventViewModel
            {
                DepositWays = depositWayes,
                Events = Events,
                HowYouKnowUs = Socialwayes,
                WhereYouFrom = places
            };
            return model;
        }

        public List<GuestEvent> GetListOfGuestEvent(DateTime dateOfEvent)
        {
            var guestEventList = _context.GuestEvents.Include(c=>c.Event).Where(c => c.DateOfEvent.Date == dateOfEvent.Date).ToList();
            return guestEventList;
        }

        public List<GuestEvent> GetGuestEventByEventId(int id)
        {
            var guestEventsById = _context.GuestEvents.Where(c => c.EventId == id).ToList();
            return guestEventsById;
        }

        public async Task<List<GuestEvent>> EditListOfGuestByDateOfEvent(int eventId, DateTime dateOfevent, decimal price)
        {
            var listOfguest = _context.GuestEvents.Where(c => c.EventId == eventId).ToList();
            if(listOfguest.Count > 0)
            {
                for(int i = 0; i<listOfguest.Count; i++)
                {
                    listOfguest[i].DateOfEvent = dateOfevent;
                    listOfguest[i].EventPrice = price;
                    listOfguest[i].GrandTotal = listOfguest[i].TotalCountOfguest * price;
                    var percentage = ((listOfguest[i].DiscountByPercentage /100) * listOfguest[i].GrandTotal);
                    listOfguest[i].LeftToPay = (listOfguest[i].GrandTotal) -(
                        listOfguest[i].PaymentCash + listOfguest[i].PaymentVisa +
                        listOfguest[i].DiscountByAmount + listOfguest[i].Deposit + percentage);

                    _context.GuestEvents.Update(listOfguest[i]);
                    await _context.SaveChangesAsync();
                }
            }
            return listOfguest;
        }
    }
}
