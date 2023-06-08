using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Iservices
{
    public interface IAreaRepository
    {
        IEnumerable<Area> GetAllAreas();
        Task<Area> GetAreaById(int id);
        Task<Area> AddArea(Area area);
        Task<Area> UpdateArea(Area area);
        Task<Area> DeleteArea(int id);

    }
}
