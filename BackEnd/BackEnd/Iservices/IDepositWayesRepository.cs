using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Iservices
{
    public interface IDepositWayesRepository
    {
        Task<DepositWay> AddWay(DepositWay model);
        Task<DepositWay> EditWay(DepositWay model);
        Task<DepositWay> GetWayById(int id);
        Task<DepositWay> DeleteWay(int id);
        Task<DepositWay> UndoDeleteWay(int id);
        List<DepositWay> GetListOfWays();
        int GetSorting();
        int GetIdByName(string name);
    }
}
