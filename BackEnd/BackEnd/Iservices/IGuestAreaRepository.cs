using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Iservices
{
    public interface IGuestAreaRepository
    {
        List<BookedGuestArea> GetAllGuestAreas(int guestId);
        Task<List<BookedGuestArea>> GetGuestAreaById(int guestId , int areaId);
        Task<BookedGuestArea> AddGuestArea(BookedGuestArea area);
        Task<List<BookedGuestArea>> AddGuestArea(List<BookedGuestArea> model);
        Task<BookedGuestArea> UpdateGuestArea(BookedGuestArea area);
        Task<BookedGuestArea> DeleteGuestArea(int guestId , int areaId);
        Task<List<BookedGuestArea>> EditGuestArea(int guestId, List<BookedGuestArea> model);
        Task<List<BookedGuestArea>> DeleteGuestArea(List<BookedGuestArea> model);
    }
}
