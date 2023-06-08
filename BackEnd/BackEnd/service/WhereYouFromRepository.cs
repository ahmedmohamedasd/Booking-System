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
    public class WhereYouFromRepository : IWhereYouFromRepository
    {
        private readonly ApplicationDbContext _context;

        public WhereYouFromRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<WhereYouFrom> AddPlace(WhereYouFrom model)
        {
            var result = _context.WhereYouFroms.Add(model);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<WhereYouFrom> DeletePlace(int id)
        {
            var guest = _context.Guests.FirstOrDefault(c => c.WhereYouId == id);
            var Place = _context.WhereYouFroms.FirstOrDefault(c => c.Id == id);
            if (guest != null)
            {
                Place.IsDeleted = true;
                _context.WhereYouFroms.Update(Place);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.WhereYouFroms.Remove(Place);
                await _context.SaveChangesAsync();
            }
            return Place;
        }

        public async Task<WhereYouFrom> EditPlace(WhereYouFrom model)
        {
            var Place = _context.WhereYouFroms.FirstOrDefault(c => c.Id == model.Id);
            if (Place != null)
            {
                Place.PlaceName = model.PlaceName;
                Place.Sorting = model.Sorting;

                _context.WhereYouFroms.Update(Place);
                await _context.SaveChangesAsync();
                return Place;
            }
            return null;
        }

        public async Task<WhereYouFrom> GetPlaceById(int id)
        {
            var Place = _context.WhereYouFroms.FirstOrDefaultAsync(c => c.Id == id);
            return await Place;
        }

        public List<WhereYouFrom> GetListOfPlaces()
        {
            return _context.WhereYouFroms.OrderBy(c => c.Sorting).ToList();
        }

        public async Task<WhereYouFrom> UndoDeletePlace(int id)
        {
            var Place = _context.WhereYouFroms.FirstOrDefault(c => c.Id == id);
            if (Place != null)
            {
                Place.IsDeleted = false;
                _context.WhereYouFroms.Update(Place);
                await _context.SaveChangesAsync();
            }
            return Place;
        }
    }
}
