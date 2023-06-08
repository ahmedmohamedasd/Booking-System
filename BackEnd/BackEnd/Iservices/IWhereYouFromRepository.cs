using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Iservices
{
    public interface IWhereYouFromRepository
    {
        Task<WhereYouFrom> AddPlace(WhereYouFrom model);
        Task<WhereYouFrom> EditPlace(WhereYouFrom model);
        Task<WhereYouFrom> GetPlaceById(int id);
        Task<WhereYouFrom> DeletePlace(int id);
        Task<WhereYouFrom> UndoDeletePlace(int id);
        List<WhereYouFrom> GetListOfPlaces();
    }
}
