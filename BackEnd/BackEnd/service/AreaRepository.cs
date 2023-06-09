﻿using BackEnd.Data;
using BackEnd.Iservices;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.service
{
    public class AreaRepository : IAreaRepository
    {
        private readonly ApplicationDbContext context;
        public AreaRepository(ApplicationDbContext _context)
        {
            this.context = _context;
        }
        public async Task<Area> AddArea(Area area)
        {
           
            var result = await context.Areas.AddAsync(area);
            await context.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<Area> UndoDeleteArea(int id)
        {
            var area = context.Areas.FirstOrDefault(c => c.Id == id);
            area.IsDeleted = false;
            context.Areas.Update(area);
            await context.SaveChangesAsync();
            return area;
        }
        public async Task<Area> DeleteArea(int id)
        {
            var model = await context.Areas.FirstOrDefaultAsync(c => c.Id == id);
            var TestExistingArea = context.BookedGuestAreas.Where(c => c.AreaId == id).ToList();
            var closedarea = context.BlockAreas.Where(c => c.AreaId == id).ToList();
            if(TestExistingArea.Count > 0 || closedarea.Count >0)
            {
                model.IsDeleted = true;
                context.Areas.Update(model);
                await context.SaveChangesAsync();
            }
            else
            {
                context.Areas.Remove(model);
                await context.SaveChangesAsync();
            }
            return model;
           
        }

        public IEnumerable<Area> GetAllAreas()
        {
            return context.Areas.OrderBy(c => c.AreaRanking);
        }

        public async Task<Area> GetAreaById(int id)
        {
            var model = await context.Areas.FirstOrDefaultAsync(c => c.Id== id);
            if (model == null)
                return null;
            return model;
        }

        public async Task<Area> UpdateArea( Area area)
        {
          
            var result = await context.Areas.FirstOrDefaultAsync(c => c.Id == area.Id);
          
                result.AreaName = area.AreaName.ToString();
                result.AreaMinimum = area.AreaMinimum;
                result.AreaRanking = area.AreaRanking;
                await context.SaveChangesAsync();
            
            return null;
        }
    }
}
