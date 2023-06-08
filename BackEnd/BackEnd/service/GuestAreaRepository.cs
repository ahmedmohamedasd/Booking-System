using BackEnd.Data;
using BackEnd.Iservices;
using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.service
{
    public class GuestAreaRepository : IGuestAreaRepository
    {
        private readonly ApplicationDbContext _context;

        public GuestAreaRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public Task<BookedGuestArea> AddGuestArea(BookedGuestArea area)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BookedGuestArea>> AddGuestArea(List<BookedGuestArea> model)
        {
            for (var i = 0; i < model.Count; i++)
            {
                _context.BookedGuestAreas.Add(model[i]);
                await _context.SaveChangesAsync();
            }
            return model;

        }

        public Task<BookedGuestArea> DeleteGuestArea(int guestId, int areaId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BookedGuestArea>> DeleteGuestArea(List<BookedGuestArea> model)
        {
            for(int i = 0; i < model.Count; i++)
            {
                _context.BookedGuestAreas.Remove(model[i]);
                await _context.SaveChangesAsync();
            }
            return model;
        }

        public async Task<List<BookedGuestArea>> EditGuestArea(int guestId, List<BookedGuestArea> model)
        {
            var areaInDb = _context.BookedGuestAreas.Where(c => c.GuestId == guestId).ToList();
            var ListOfAreas = model;
            if(areaInDb.Count > 0)
            {
                for(int i=0; i < areaInDb.Count; i++)
                {
                    var index = ListOfAreas.IndexOf(ListOfAreas.Find(c => c.AreaId == areaInDb[i].AreaId));
                    if(index != -1)
                    {
                        model.Remove(model[index]);
                    }
                    else
                    {
                        _context.BookedGuestAreas.Remove(areaInDb[i]);
                       await _context.SaveChangesAsync();
                    }
                }
                if (model.Count > 0)
                {
                    for(int i = 0; i < model.Count; i++)
                    {
                        _context.BookedGuestAreas.Add(model[i]);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            return areaInDb;
        }

        public List<BookedGuestArea> GetAllGuestAreas(int guestId)
        {
            var guestarea = _context.BookedGuestAreas.Where(c => c.GuestId == guestId).ToList();
            return guestarea;
        }

        public Task<List<BookedGuestArea>> GetGuestAreaById(int guestId, int areaId)
        {
            throw new NotImplementedException();
        }

        public Task<BookedGuestArea> UpdateGuestArea(BookedGuestArea area)
        {
            throw new NotImplementedException();
        }
    }
}
