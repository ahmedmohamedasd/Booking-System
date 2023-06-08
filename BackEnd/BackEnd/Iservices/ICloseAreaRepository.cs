using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Iservices
{
    public interface ICloseAreaRepository
    {
        Task<BlockArea[]> AddClosedArea(BlockArea[] model);
        List<BlockArea> GetListOfClosedArea(DateTime DateOfBlocked);
        Task<List<BlockArea>> DeleteClosedArea(DateTime DateOfBlocked);
    }
}
