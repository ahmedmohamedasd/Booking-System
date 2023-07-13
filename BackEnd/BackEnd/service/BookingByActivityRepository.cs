using BackEnd.Data;
using BackEnd.Iservices;
using BackEnd.Models;
using BackEnd.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.service
{
    public class BookingByActivityRepository : IBookingByActivityRepository
    {
        private readonly ApplicationDbContext _context;
        
        public BookingByActivityRepository(ApplicationDbContext context )
        {
            _context = context;
           
            
        }
        public Task<List<GuestActivity>> AddGuestActivity(List<GuestActivity> model)
        {
            throw new NotImplementedException();
        }

        public Task<GuestActivity> DeleteGuestActivity(List<GuestActivity> model)
        {
            throw new NotImplementedException();
        }

        public Task<List<GuestActivity>> EditGuestActivity(List<GuestActivity> model)
        {
            throw new NotImplementedException();
        }

        public ActivityViewModel GetActivityViewModel()
        {
            // ActivityRepository activity = new ActivityRepository(_context);
            List<Activity> activities = _context.Activitys.OrderBy(c=>c.Sorting).ToList();
           
            List<ActivityModel> activityModels = new List<ActivityModel>();
            if(activities.Count() > 0)
            {   
                for(int i = 0; i < activities.Count(); i++)
                {
                    ActivityModel AM = new ActivityModel
                    {
                        ActivityName = activities[i].ActivityName,
                        ActivityPrice = activities[i].ActivityPrice,
                        Id = activities[i].Id,
                        IsIncluded = false,
                        Quantity = 0,
                        Sorting = activities[i].Sorting,
                        SubTotal =0

                    };
                    activityModels.Add(AM);
                }
            }
            var depositWayList = _context.DepositWays.Where(c => c.IsDeleted == false).OrderBy(c => c.Sorting).ToList();
            var whereYouFromList = _context.WhereYouFroms.Where(c => c.IsDeleted == false).OrderBy(c=>c.Sorting).ToList();
            var howYouKnowUsList = _context.HowYouKnowUss.Where(c => c.IsDeleted == false).OrderBy(c=>c.Sorting).ToList();
            var model = new ActivityViewModel
            {
                Activities = activityModels,
                DepositWays = depositWayList,
                HowYouKnowUs = howYouKnowUsList,
                WhereYouFroms = whereYouFromList
            };
            
            return model;
        }

        public IEnumerable<Guest> GetAllBookedCompany()
        {
            throw new NotImplementedException();
        }

        public EditActivityViewModel GetEditActivityViewModel(int guestId)
        {
            var guest = _context.Guests.Where(c => c.Id == guestId).FirstOrDefault();
            var whereYouFrom = _context.WhereYouFroms.Where(c => c.IsDeleted == false || c.Id == guest.WhereYouId).OrderBy(c=>c.Sorting).ToList();
            var HowYouKnowUs = _context.HowYouKnowUss.Where(c => c.IsDeleted == false || c.Id == guest.KnowUsId).OrderBy(c=>c.Sorting).ToList();
            var DepositWayes = _context.DepositWays.Where(c => c.IsDeleted == false || c.Id == guest.DepositWayId).OrderBy(c => c.Sorting).ToList();
            var GuestActivities = _context.GuestActivities.Where(c => c.GuestId == guestId).ToList();
            var activities = _context.Activitys.OrderBy(c=>c.Sorting).ToList();
            List<ActivityModel> activityModels = new List<ActivityModel>();
            for(int i = 0; i < activities.Count; i++)
            {
                var am = new ActivityModel
                {
                    Id = activities[i].Id,
                    ActivityName = activities[i].ActivityName,
                    ActivityPrice = activities[i].ActivityPrice,
                    Quantity = 0,
                    IsIncluded = false,
                    Sorting = activities[i].Sorting,
                    SubTotal = 0,

                };
                activityModels.Add(am);
            }
            if(activityModels.Count > 0)
            {
                for(int i = 0; i < activityModels.Count; i++)
                {
                    var index = GuestActivities.IndexOf(GuestActivities.Find(c => c.ActivityId == activityModels[i].Id));
                    if(index != -1)
                    {
                        activityModels[i].ActivityPrice = (decimal)GuestActivities[index].ActivityPrice;
                        activityModels[i].IsIncluded = GuestActivities[index].IsIncluded;
                        activityModels[i].Quantity = GuestActivities[index].Quantity;
                        activityModels[i].SubTotal = GuestActivities[index].SubTotal;
                    }
                }
            }
            EditActivityViewModel model = new EditActivityViewModel
            {
                Activities = activityModels,
                DepositWays = DepositWayes,
                HowYouKnowUs = HowYouKnowUs,
                WhereYouFroms = whereYouFrom

            };
            return model;
        }

        public Task<Guest> GetGuestById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
