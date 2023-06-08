using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Iservices
{
    public interface IGuestActivityRepository
    {
        Task<List<GuestActivity>> AddGuestActivity(List<GuestActivity> model);
        List<GuestActivity> GetListOfGuestActivities(int guestId);
        Task<int> EditGuestActivityDateBooking(int guestId, DateTime DateOfBooking);
        Task<GuestActivity> EditGuestActivity(int guestId, GuestActivity model);
        Task<GuestActivity> DeleteGuestActivity(int guestId, int activityId);
        Task<List<GuestActivity>> DeleteGuestActivity(int guestId);

    }
}
