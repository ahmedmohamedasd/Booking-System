using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Iservices
{
   public interface IActivityRepository
    {
        Task<Activity> AddActivity(Activity model);
        Task<Activity> EditActivity(Activity model);
        Task<Activity> GetActivityById(int id);
        Task<Activity> DeleteActivity(int id);
        Task<Activity> UndoDeleteActivity(int id);
        List<Activity> GetListOfActivities();
        

    }
}
