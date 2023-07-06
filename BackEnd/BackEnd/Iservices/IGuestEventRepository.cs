using BackEnd.Models;
using BackEnd.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Iservices
{
    public interface IGuestEventRepository
    {
        AddGuestEventViewModel GetGuestEventViewModel();
        Task<GuestEvent> AddguestEvent(GuestEvent model);
        List<GuestEvent> GetListOfGuestEvent(DateTime dateOfEvent);
        Task<GuestEvent> GetGuestEventById(int id);
        Task<GuestEvent> EditGuestevent(GuestEvent model);
        EditGuestEventViewModel GetEditGuestEventViewModel(int id);
        Task<GuestEvent> CancelGuestEvent(int id);
        Task<GuestEvent> DeleteGuestEvent(int id);
        Task<GuestEvent> UndoCancelGuestEvent(int id);
        List<GuestEvent> GetGuestEventByEventId(int id);
        Task<List<GuestEvent>> EditListOfGuestByDateOfEvent(int eventId, DateTime dateOfevent, decimal price);
    }
}
