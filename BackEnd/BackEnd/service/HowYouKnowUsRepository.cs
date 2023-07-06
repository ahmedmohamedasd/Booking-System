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
    public class HowYouKnowUsRepository : IHowYouKnowUsRepository
    {
        private readonly ApplicationDbContext _context;

        public HowYouKnowUsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<HowYouKnowUs> AddWay(HowYouKnowUs model)
        {
            var result = _context.HowYouKnowUss.Add(model);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<HowYouKnowUs> DeleteWay(int id)
        {
            var guest = _context.Guests.FirstOrDefault(c => c.KnowUsId == id);
            var way = _context.HowYouKnowUss.FirstOrDefault(c => c.Id == id);
            if (guest != null)
            {
                way.IsDeleted = true;
                _context.HowYouKnowUss.Update(way);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.HowYouKnowUss.Remove(way);
                await _context.SaveChangesAsync();
            }
            return way;
        }

        public async Task<HowYouKnowUs> EditWay(HowYouKnowUs model)
        {
            var way = _context.HowYouKnowUss.FirstOrDefault(c => c.Id == model.Id);
            if (way != null)
            {
                way.WayName = model.WayName;
                way.Sorting = model.Sorting;
                
                _context.HowYouKnowUss.Update(way);
                await _context.SaveChangesAsync();
                return way;
            }
            return null;
        }

        public async Task<HowYouKnowUs> GetWayById(int id)
        {
            var way = _context.HowYouKnowUss.FirstOrDefaultAsync(c => c.Id == id);
            return await way;
        }

        public List<HowYouKnowUs> GetListOfWays()
        {
            return _context.HowYouKnowUss.OrderBy(c => c.Sorting).ToList();
        }

        public async Task<HowYouKnowUs> UndoDeleteWay(int id)
        {
            var way = _context.HowYouKnowUss.FirstOrDefault(c => c.Id == id);
            if (way != null)
            {
                way.IsDeleted = false;
                _context.HowYouKnowUss.Update(way);
                await _context.SaveChangesAsync();
            }
            return way;
        }

        public int GetSorting()
        {
            var sorting = _context.HowYouKnowUss.OrderByDescending(c => c.Sorting).FirstOrDefault().Sorting;
            return sorting;
        }
        public int GetIdByName(string name)
        {
            var id = _context.HowYouKnowUss.FirstOrDefault(c => c.WayName.ToLower() == name.ToLower()).Id;
            return id;
        }
    }
}
