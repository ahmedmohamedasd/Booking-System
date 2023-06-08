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
    public class ActivityRepository : IActivityRepository
    {
        private readonly ApplicationDbContext _context;

        public ActivityRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Activity> AddActivity(Activity model)
        {
            var result = _context.Activitys.Add(model);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Activity> DeleteActivity(int id)
        {
            var guestActivities = _context.GuestActivities.FirstOrDefault(c => c.ActivityId == id);
            var activity = _context.Activitys.FirstOrDefault(c => c.Id == id);
            if (guestActivities != null)
            {
                activity.IsDeleted = true;
                _context.Activitys.Update(activity);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.Activitys.Remove(activity);
                await _context.SaveChangesAsync();
            }
            return activity;
        }

        public async Task<Activity> EditActivity( Activity model)
        {
            var activity = _context.Activitys.FirstOrDefault(c => c.Id == model.Id);
            if(activity != null)
            {
                activity.ActivityPrice = model.ActivityPrice;
                activity.ActivityName = model.ActivityName;
                activity.Sorting = model.Sorting;
                _context.Activitys.Update(activity);
                await _context.SaveChangesAsync();
                return activity;
            }
            return null;
        }

        public async Task<Activity> GetActivityById(int id)
        {
            var activity = _context.Activitys.FirstOrDefaultAsync(c => c.Id == id);
            return await activity;
        }

        public List<Activity> GetListOfActivities()
        {
           return _context.Activitys.OrderBy(c => c.Sorting).ToList();
        }

        public async Task<Activity> UndoDeleteActivity(int id)
        {
            var activity = _context.Activitys.FirstOrDefault(c => c.Id == id);
            if(activity != null)
            {
                activity.IsDeleted = false;
                _context.Activitys.Update(activity);
                await _context.SaveChangesAsync();
            }
            return activity;
        }
    }
}
