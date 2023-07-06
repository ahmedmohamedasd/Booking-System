using BackEnd.Iservices;
using BackEnd.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsRepository _statisticsRepository;
        public StatisticsController(IStatisticsRepository statisticsRepository)
        {
            this._statisticsRepository = statisticsRepository;
        }
        // GET: api/<StatisticsController>
        [HttpGet("GetPlaceStatistics/{dateOfFrom}/{dateOfTo}")]
        public IEnumerable<PlaceStatistics> GetPlaceStatistics(string dateOfFrom , string  dateOfTo)
        {
            DateTime DateFrom = DateTime.ParseExact(dateOfFrom, "MM-dd-yyyy", null);
            DateTime DateTo = DateTime.ParseExact(dateOfTo, "MM-dd-yyyy", null);

            var model = _statisticsRepository.GetPlaceStatistics(DateFrom, DateTo);
            return model;
        }
        [HttpGet("GetSocialStatistics/{dateOfFrom}/{dateOfTo}")]
        public IEnumerable<SocialWayStatistics> GetSocialWayStatistics(string dateOfFrom, string dateOfTo)
        {
            DateTime DateFrom = DateTime.ParseExact(dateOfFrom, "MM-dd-yyyy", null);
            DateTime DateTo = DateTime.ParseExact(dateOfTo, "MM-dd-yyyy", null);

            var model = _statisticsRepository.GetSocialStatistics(DateFrom, DateTo);
            return model;
        }

        [HttpGet("GetTicketStatistics/{dateOfFrom}/{dateOfTo}")]
        public IEnumerable<TicketStatistics> GetTicketStatistics(string dateOfFrom, string dateOfTo)
        {
            DateTime DateFrom = DateTime.ParseExact(dateOfFrom, "MM-dd-yyyy", null);
            DateTime DateTo = DateTime.ParseExact(dateOfTo, "MM-dd-yyyy", null);

            var model = _statisticsRepository.GetTicketStatistics(DateFrom, DateTo);
            return model;
        }
        [HttpGet("GetActivityStatistics/{dateOfFrom}/{dateOfTo}")]
        public IEnumerable<ActivityStatistics> GetActivityStatistics(string dateOfFrom, string dateOfTo)
        {
            DateTime DateFrom = DateTime.ParseExact(dateOfFrom, "MM-dd-yyyy", null);
            DateTime DateTo = DateTime.ParseExact(dateOfTo, "MM-dd-yyyy", null);

            var model = _statisticsRepository.GetActivityStatistics(DateFrom, DateTo);
            return model;
        }
        [HttpGet("GetGuestStatistics/{dateOfFrom}/{dateOfTo}")]
        public IEnumerable<GuestStatistics> GetGuestStatistics(string dateOfFrom, string dateOfTo)
        {
            DateTime DateFrom = DateTime.ParseExact(dateOfFrom, "MM-dd-yyyy", null);
            DateTime DateTo = DateTime.ParseExact(dateOfTo, "MM-dd-yyyy", null);

            var model = _statisticsRepository.GetGuestStatistics(DateFrom, DateTo);
            return model;
        }
        [HttpGet("GetGatheringStatistics/{dateOfFrom}/{dateOfTo}/{bookTypeId}")]
        public IEnumerable<Statistics> GetGatheringStatistics(string dateOfFrom, string dateOfTo , int bookTypeId)
        {
            DateTime DateFrom = DateTime.ParseExact(dateOfFrom, "MM-dd-yyyy", null);
            DateTime DateTo = DateTime.ParseExact(dateOfTo, "MM-dd-yyyy", null);

            var model = _statisticsRepository.GetGatheringStatistics(DateFrom, DateTo , bookTypeId);
            return model;
        }





    }
}
