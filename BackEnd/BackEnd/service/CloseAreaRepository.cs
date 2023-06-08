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
    public class CloseAreaRepository : ICloseAreaRepository
    {
        private readonly ApplicationDbContext _context;
        public CloseAreaRepository(ApplicationDbContext _context)
        {
            this._context = _context;
        }
        public async Task<BlockArea[]> AddClosedArea(BlockArea[] model)
        {
            for(int i = 0; i < model.Length; i++)
            {
              await  _context.BlockAreas.AddAsync(model[i]);
              await  _context.SaveChangesAsync();
            }
            return model;
        }

        public async Task<List<BlockArea>> DeleteClosedArea(DateTime DateOfBlocked)
        {
            var closedArea =  _context.BlockAreas.Where(c => c.DateOfBlock == DateOfBlocked).ToList();
            if (closedArea.Count > 0)
            {
                for(int i = 0; i < closedArea.Count; i++)
                {
                    _context.BlockAreas.Remove(closedArea[i]);
                    await _context.SaveChangesAsync();
                }
            }
            return closedArea;
        }

        public List<BlockArea> GetListOfClosedArea(DateTime DateOfBlocked)
        {
            var closedArea = _context.BlockAreas.Where(c => c.DateOfBlock == DateOfBlocked).ToList();
            return closedArea;

        }
    }
}
