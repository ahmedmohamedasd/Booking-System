using BackEnd.Models;
using BackEnd.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Iservices
{
    public interface IBookingRepository
    {
        IEnumerable<AreaViewModel> GetBookedArea(DateTime date);
        IEnumerable<BookingType> GetListOfBookingType();
        TicketViewModel GetTicketviewModel();
        EditTicketViewModel GetEditTicketViewModel(int id);

        int AddNewPlace(string  name);
        int AddNewSocialWay(string name);
        int AddNewGuest(Guest model);
    
        Task<List<BookedGuestArea>> AddBookedGuestArea(List<BookedGuestArea> model);
        IEnumerable<listGuestViewModel> GetListOfGuests(DateTime date);
        IEnumerable<AreaViewModel> GetSelectedBookedArea(EditSelectedAreaViewModel model);

        Task<Guest> EditGuest(int guestId , DateTime date);
        Task<Guest> EditGuest(Guest model);
    }
}
