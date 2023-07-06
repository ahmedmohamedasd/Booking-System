using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Iservices
{
    public interface IHowYouKnowUsRepository
    {
        Task<HowYouKnowUs> AddWay(HowYouKnowUs model);
        Task<HowYouKnowUs> EditWay(HowYouKnowUs model);
        Task<HowYouKnowUs> GetWayById(int id);
        Task<HowYouKnowUs> DeleteWay(int id);
        Task<HowYouKnowUs> UndoDeleteWay(int id);
        List<HowYouKnowUs> GetListOfWays();
        int GetSorting();
        int GetIdByName(string name);
    }
}
