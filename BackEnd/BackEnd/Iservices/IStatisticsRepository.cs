using BackEnd.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Iservices
{
    public interface IStatisticsRepository
    {
        IEnumerable<PlaceStatistics> GetPlaceStatistics(DateTime dateFrom, DateTime dateTo);
        IEnumerable<SocialWayStatistics> GetSocialStatistics(DateTime dateFrom, DateTime dateTo);
        IEnumerable<TicketStatistics> GetTicketStatistics(DateTime dateFrom, DateTime dateTo);
        IEnumerable<ActivityStatistics> GetActivityStatistics(DateTime dateFrom, DateTime dateTo);
        IEnumerable<GuestStatistics> GetGuestStatistics(DateTime dateFrom, DateTime dateTo);

    }
}
