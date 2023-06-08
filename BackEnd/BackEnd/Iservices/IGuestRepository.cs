using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Iservices
{
    public interface IGuestRepository
    {
        Task<Guest> AddGuest(Guest model);
        Task<Guest> EditGuest(Guest model);
        Task<bool> DeleteGuest(int id);
        Task<bool> CancelGuest(int id);
        Task<bool> UndoCancelGuest(int id);
        Guest GetGuestById(int id);
        IEnumerable<Guest> GetListOfGuests();
    }
}
