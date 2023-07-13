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
    public class DepositWayRepository:IDepositWayesRepository
    {
        private readonly ApplicationDbContext _context;

        public DepositWayRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<DepositWay> AddWay(DepositWay model)
        {
            var result = _context.DepositWays.Add(model);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<DepositWay> DeleteWay(int id)
        {
            var guest = _context.Guests.FirstOrDefault(c => c.DepositWayId == id);
            var way = _context.DepositWays.FirstOrDefault(c => c.Id == id);
            if (guest != null)
            {
                way.IsDeleted = true;
                _context.DepositWays.Update(way);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.DepositWays.Remove(way);
                await _context.SaveChangesAsync();
            }
            return way;
        }

        public async Task<DepositWay> EditWay(DepositWay model)
        {
            var way = _context.DepositWays.FirstOrDefault(c => c.Id == model.Id);
            if (way != null)
            {
                way.Name = model.Name;
                way.Sorting = model.Sorting;

                _context.DepositWays.Update(way);
                await _context.SaveChangesAsync();
                return way;
            }
            return null;
        }

        public async Task<DepositWay> GetWayById(int id)
        {
            var way = _context.DepositWays.FirstOrDefaultAsync(c => c.Id == id);
            return await way;
        }

        public List<DepositWay> GetListOfWays()
        {
            return _context.DepositWays.OrderBy(c => c.Sorting).ToList();
        }

        public async Task<DepositWay> UndoDeleteWay(int id)
        {
            var way = _context.DepositWays.FirstOrDefault(c => c.Id == id);
            if (way != null)
            {
                way.IsDeleted = false;
                _context.DepositWays.Update(way);
                await _context.SaveChangesAsync();
            }
            return way;
        }

        public int GetSorting()
        {
            var sorting = _context.DepositWays.OrderByDescending(c => c.Sorting).FirstOrDefault().Sorting;
            return sorting;
        }

        public int GetIdByName(string name)
        {
            var id = _context.DepositWays.FirstOrDefault(c => c.Name.ToLower() == name.ToLower()).Id;
            return id;
        }

    }
}
