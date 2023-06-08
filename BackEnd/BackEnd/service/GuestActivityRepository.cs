using BackEnd.Data;
using BackEnd.Iservices;
using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.service
{
    public class GuestActivityRepository : IGuestActivityRepository
    {
        private readonly ApplicationDbContext _context;
        public GuestActivityRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<List<GuestActivity>> AddGuestActivity(List<GuestActivity> model)
        {
           for(int i = 0; i < model.Count; i++)
            {
                _context.GuestActivities.Add(model[i]);
                await _context.SaveChangesAsync();
            }
            return model;
        }

        public async Task<GuestActivity> DeleteGuestActivity(int guestId, int activityId)
        {
            var guestActivity = _context.GuestActivities.FirstOrDefault(c => c.GuestId == guestId && c.ActivityId == activityId);
            if (guestActivity != null)
            {
                _context.GuestActivities.Remove(guestActivity);
                await _context.SaveChangesAsync();
            }
            return guestActivity;
        }

        public async Task<List<GuestActivity>> DeleteGuestActivity(int guestId)
        {
            var guestActivityInDb = _context.GuestActivities.Where(c => c.GuestId == guestId).ToList();
            if(guestActivityInDb.Count > 0)
            {
                for(int i = 0; i < guestActivityInDb.Count; i++)
                {
                    _context.GuestActivities.Remove(guestActivityInDb[i]);
                    await _context.SaveChangesAsync();
                }
            }
            return guestActivityInDb;
        }

        public async Task<GuestActivity> EditGuestActivity(int guestId, GuestActivity model)
        {
            var guestActivitiInDb = _context.GuestActivities
                                   .FirstOrDefault(c => c.GuestId == guestId && 
                                                  c.ActivityId == model.ActivityId);
            if(guestActivitiInDb != null)
            {
                guestActivitiInDb.Quantity = model.Quantity;
                guestActivitiInDb.IsIncluded = model.IsIncluded;
                guestActivitiInDb.SubTotal = model.SubTotal;
                _context.GuestActivities.Update(guestActivitiInDb);
               await _context.SaveChangesAsync();
            }
            return model;
        }

        public async Task<int> EditGuestActivityDateBooking(int guestId, DateTime DateOfBooking)
        {
            var model = _context.GuestActivities.Where(c => c.GuestId == guestId).ToList();
            if(model.Count > 0)
            {
                for (int i = 0; i < model.Count; i++)
                {
                    model[i].DateOfBooking = DateOfBooking;
                    _context.GuestActivities.Update(model[i]);
                    await _context.SaveChangesAsync();
                }
                return guestId;
            }
           
            return guestId;
        }

        public List<GuestActivity> GetListOfGuestActivities(int guestId)
        {
            var model = _context.GuestActivities.Where(c => c.GuestId == guestId).ToList();
            return model;
        }
    }
}
