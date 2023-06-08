using BackEnd.Models;
using BackEnd.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Iservices
{
   public interface IBookingByActivityRepository
    {
        IEnumerable<Guest> GetAllBookedCompany();
        Task<Guest> GetGuestById(int id);
        Task<List<GuestActivity>> AddGuestActivity(List<GuestActivity> model);
        Task<List<GuestActivity>> EditGuestActivity(List<GuestActivity> model);
        Task<GuestActivity> DeleteGuestActivity(List<GuestActivity> model);
        EditActivityViewModel GetEditActivityViewModel(int guestId);
        ActivityViewModel GetActivityViewModel();
    
    }
}
