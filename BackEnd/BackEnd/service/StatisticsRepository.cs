using BackEnd.Data;
using BackEnd.Iservices;
using BackEnd.ViewModel;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.service
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly ApplicationDbContext _context;
        
        public StatisticsRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

      

        public IEnumerable<PlaceStatistics> GetPlaceStatistics(DateTime dateFrom, DateTime dateTo)
        {

            var listOfGuest = _context.Guests.Where(c => c.DateOfBooking >= dateFrom && c.DateOfBooking <= dateTo && c.IsCanceled == false).ToList();
            IEnumerable<PlaceStatistics> model = _context.Guests
                    .Where(c => c.DateOfBooking >= dateFrom 
                                && c.DateOfBooking <= dateTo 
                                && c.IsCanceled == false 
                                && c.BookingTypeId == 1)
                    .GroupBy(p => new { p.WhereYouFrom.PlaceName })
                    .Select(c => new PlaceStatistics
                    {
                        Name = c.Key.PlaceName,
                        Value = c.Count()

                    });
            return model;
        }

        public IEnumerable<SocialWayStatistics> GetSocialStatistics(DateTime dateFrom, DateTime dateTo)
        {
            IEnumerable<SocialWayStatistics> model = _context.Guests
                    .Where(c => c.DateOfBooking >= dateFrom 
                                && c.DateOfBooking <= dateTo 
                                && c.IsCanceled == false
                                && c.BookingTypeId == 1)
                    .GroupBy(p => new { p.HowYouKnowUs.WayName })
                    .Select(c => new SocialWayStatistics
                    {
                        Name = c.Key.WayName,
                        Value = c.Count()
                    });
            return model;
        }

        public IEnumerable<TicketStatistics> GetTicketStatistics(DateTime dateFrom, DateTime dateTo)
        {
            var Testguest = _context.GuestTickets.Where(c => c.Guest.IsCanceled == false).ToList();
            IEnumerable<TicketStatistics> model = _context.GuestTickets
                   .Where(c => c.DateOfBooking >= dateFrom && c.DateOfBooking <= dateTo && c.Guest.IsCanceled == false)
                   .GroupBy(p => new { p.Ticket.TicketName })
                   .Select(c => new TicketStatistics
                   {
                       Name = c.Key.TicketName,
                       Value = c.Sum(c=>c.CountOfAdult + c.CountOfKids)
                   });
            return model;
        }
        public IEnumerable<ActivityStatistics> GetActivityStatistics(DateTime dateFrom, DateTime dateTo)
        {
            IEnumerable<ActivityStatistics> model = _context.GuestActivities
                    .Where(c => c.DateOfBooking >= dateFrom && c.DateOfBooking <= dateTo && c.Guest.IsCanceled == false)
                    .GroupBy(p => new { p.Activity.ActivityName })
                    .Select(c => new ActivityStatistics
                    {
                        Name = c.Key.ActivityName,
                        Value = c.Sum(c => c.Quantity)
                    });
            return model;
        }

        public IEnumerable<GuestStatistics> GetGuestStatistics(DateTime dateFrom, DateTime dateTo)
        {
            List<GuestTestModel> model = _context.Guests
                    .Where(c => c.DateOfBooking >= dateFrom && c.DateOfBooking <= dateTo && c.IsCanceled == false)
                    .GroupBy(p => new { p.DateOfBooking.Month, p.BookingType.Name })
                    .Select(c => new GuestTestModel
                    {
                        Date = c.Key.Month,
                        BookTypeName = c.Key.Name,
                        CountOfGuest = c.Sum(c => c.TotalCountOfguest)

                    }).OrderBy(c=>c.Date).ToList();
            var testModel = model;
            List<GuestStatistics> guests = new List<GuestStatistics>();

            for (int i = 0; i < model.Count; i++)
            {
                var listofIndex = testModel.Where(c => c.Date == model[i].Date).ToList();
                if(listofIndex.Count > 0)
                {
                    
                    List<SeriesStatistics> seriesStatistics = new List<SeriesStatistics>();
                    for (int k = 0; k < listofIndex.Count; k++)
                    {
                        var ser = new SeriesStatistics
                        {
                            Name = listofIndex[k].BookTypeName,
                            Value = listofIndex[k].CountOfGuest
                        };
                        seriesStatistics.Add(ser);
                      
                    }
                  
                    var ss = new GuestStatistics
                    {
                        Name = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(model[i].Date),
                    Series = seriesStatistics
                    };
                    guests.Add(ss);
                    for (int z = 0; z < listofIndex.Count;z++)
                    {
                        var index = testModel.IndexOf(
                                    testModel.Find(c => c.Date == listofIndex[z].Date 
                                                     && c.BookTypeName == listofIndex[z].BookTypeName));
                        testModel.RemoveAt(index);

                    }
                }       
               
            }
            return guests;

        }
    }
}
