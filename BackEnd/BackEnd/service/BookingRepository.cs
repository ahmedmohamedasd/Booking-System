using BackEnd.Data;
using BackEnd.Iservices;
using BackEnd.Models;
using BackEnd.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.service
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext context;
        public BookingRepository(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        public async Task<List<BookedGuestArea>> AddBookedGuestArea(List<BookedGuestArea> model)
        {
            for(var i = 0; i < model.Count; i++)
            {
                context.BookedGuestAreas.Add(model[i]);
                await context.SaveChangesAsync();
            }
            return model;
          
        }

        public int AddNewGuest(Guest guest)
        {
            context.Guests.Add(guest);
            context.SaveChanges();
            var GuestId = context.Guests.Where(c => c.Name == guest.Name && c.DateOfBooking == guest.DateOfBooking && c.Phone == guest.Phone).FirstOrDefault().Id;
            
            return  GuestId;
        }

        public int AddNewSocialWay(string name)
        {
            HowYouKnowUs model = new HowYouKnowUs
            {
                WayName = name
               
            };
            context.HowYouKnowUss.Add(model);
            context.SaveChanges();
            var id = context.HowYouKnowUss.Where(c => c.WayName.ToLower() == name.ToLower()).FirstOrDefault().Id;
            return id;
        }

        public int AddNewPlace (string name)
        {
            WhereYouFrom model = new WhereYouFrom
            {
                PlaceName = name,
                Id = 0
            };
            context.WhereYouFroms.Add(model);
            context.SaveChanges();
            var id = context.WhereYouFroms.Where(c => c.PlaceName.ToLower() == name.ToLower()).FirstOrDefault().Id;
            return id;
        }

        public IEnumerable<AreaViewModel> GetBookedArea(DateTime date)
        {
            var model = ListOfBookedArea(date);
            return model.OrderBy(c => c.Ranking);
        }
        private List<AreaViewModel> ListOfBookedArea(DateTime date)
        {
            List<AreaViewModel> model = new List<AreaViewModel>();
            var bookedAreas = context.BookedGuestAreas.Where(c => c.DateOfBooking.Date == date.Date)
                                                      .Include(c => c.Guest)
                                                      .ToList();
            var BlockedArea = context.BlockAreas.Where(c => c.DateOfBlock.Date == date.Date).ToList();
            var listofArea = context.Areas.ToList();
            for (int i = 0; i < listofArea.Count; i++)
            {
                AreaViewModel ss = new AreaViewModel
                {
                    AreaName = listofArea[i].AreaName,
                    Ranking = listofArea[i].AreaRanking,
                    Closed = false,
                    GuestName = null,
                    hasBooked = false,
                    Note = null,
                    IsSelected = false,
                    AreaId = listofArea[i].Id,
                    DateOfBooking = date
                };
                model.Add(ss);
            }
            if (BlockedArea.Count > 0)
            {
                for (int i = 0; i < BlockedArea.Count; i++)
                {
                    var indexOf = model.IndexOf(model.Find(c => c.AreaId == BlockedArea[i].AreaId));
                    model[indexOf].Closed = true;
                    model[indexOf].Note = BlockedArea[i].Note;
                }
            }
            if (bookedAreas.Count > 0)
            {
                for (int i = 0; i < bookedAreas.Count; i++)
                {
                    var indexOf = model.IndexOf(model.Find(c => c.AreaId == bookedAreas[i].AreaId));
                    model[indexOf].hasBooked = true;
                    model[indexOf].GuestName = bookedAreas[i].Guest.Name;

                }
            }
            return model;

        }
        public IEnumerable<AreaViewModel> GetSelectedBookedArea(EditSelectedAreaViewModel editSelected)
        {
            List<AreaViewModel> model = new List<AreaViewModel>();
            if(editSelected.CurrentDate.Date == editSelected.BookedGuestAreas[0].DateOfBooking.Date)
            {
                 model = ListOfBookedArea(editSelected.CurrentDate);
                for (int i = 0; i < editSelected.BookedGuestAreas.Count; i++)
                {
                    var indexOf = model.IndexOf(model.Find(c => c.AreaId == editSelected.BookedGuestAreas[i].AreaId));
                    model[indexOf].Closed = true;
                    model[indexOf].IsSelected = true;
                }
            }
            else
            {
                model = ListOfBookedArea(editSelected.CurrentDate);
            }
            
            return model.OrderBy(c => c.Ranking);
        }
        public IEnumerable<BookingType> GetListOfBookingType()
        {
            return context.BookingTypes.ToList();
        }

        public TicketViewModel GetTicketviewModel()
        {
            var ticketList = context.tickets.OrderBy(c=>c.Sorting).ToList();
            var depositWayList = context.DepositWays.Where(c => c.IsDeleted == false).OrderBy(c => c.Sorting).ToList();
            var whereYouFromList = context.WhereYouFroms.Where(c => c.IsDeleted == false).OrderBy(c=>c.Sorting).ToList();
            var howYouKnowUsList = context.HowYouKnowUss.Where(c => c.IsDeleted == false).OrderBy(c=>c.Sorting).ToList();
            var model = new TicketViewModel
            {
                Tickets = ticketList,
                WhereYouFroms = whereYouFromList,
                DepositWays = depositWayList,
                HowYouKnowUs = howYouKnowUsList
            };
            return model;
        }
        public IEnumerable<listGuestViewModel> GetListOfGuests(DateTime date)
        {
            List<listGuestViewModel> model = new List<listGuestViewModel>();
            var guests = context.Guests.Where(c => c.DateOfBooking.Date == date.Date).ToList();
            if(guests.Count != 0)
            {
                for(var i = 0; i < guests.Count; i++)
                {
                    listGuestViewModel ss = new listGuestViewModel();
                    if (guests[i].BookingTypeId == 1)
                    {
                        List<GuestTicketViewModel> gtvm = new List<GuestTicketViewModel>();
                        var tickets = context.GuestTickets.Include(c=>c.Ticket).AsNoTracking().Where(c => c.GuestId == guests[i].Id).ToList();
                        for(int k = 0; k < tickets.Count; k++)
                        {
                            var tvm = new GuestTicketViewModel
                            {
                                TicketName = tickets[k].Ticket.TicketName,
                                CountOfAdult = tickets[k].CountOfAdult,
                                CountOfKids = tickets[k].CountOfKids,
                                PriceForAdult = (decimal)tickets[k].PriceForAdult,
                                PriceForKids = (decimal)tickets[k].PriceForKids,
                                SubTotal = (decimal)((tickets[k].CountOfAdult * tickets[k].PriceForAdult) +
                                (tickets[k].CountOfKids * tickets[k].PriceForKids))
                            };
                            gtvm.Add(tvm);

                        }
                        
                        ss.GuestTickets = gtvm;
                    }
                    else
                    {
                        var activities = context.GuestActivities.Include(c=>c.Activity).Where(c => c.GuestId == guests[i].Id).ToList();
                        List<GuestActivityViewModel> gavm = new List<GuestActivityViewModel>();
                        for(int k = 0; k < activities.Count; k++)
                        {
                            var vm = new GuestActivityViewModel
                            {
                                ActivityName = activities[k].Activity.ActivityName,
                                Quantity = activities[k].Quantity,
                                IsIncluded = activities[k].IsIncluded,
                                Price = (decimal)activities[k].ActivityPrice
                            };
                            gavm.Add(vm);
                        }
                        
                        ss.GuestActivities = gavm;
                    }
                    var areas = context.BookedGuestAreas.AsNoTracking().Where(c => c.GuestId == guests[i].Id && c.DateOfBooking.Date == guests[i].DateOfBooking.Date).ToList();
                    ss.GuestAreas = areas;
                    ss.Guests = guests[i];
                    model.Add(ss);
                }
            }
            return model;
        }
      
        public async Task<Guest> EditGuest(int guestId, DateTime date)
        {
            var guest = context.Guests.FirstOrDefault(c => c.Id == guestId);
            if(guest != null)
            {
                guest.DateOfBooking = date;
                context.Guests.Update(guest);
                await context.SaveChangesAsync();
            }
            return guest;
        }

        public EditTicketViewModel GetEditTicketViewModel(int guestId)
        {
            var guest = context.Guests.Where(c => c.Id == guestId).FirstOrDefault();
            List<TicketModel> ticketModels = new List<TicketModel>();
            var ticketList = context.tickets.ToList();
            var guestTickets = context.GuestTickets.Where(c => c.GuestId == guestId).ToList();

            var depositWayList = context.DepositWays.Where(c=>c.IsDeleted == false || c.Id == guest.DepositWayId).OrderBy(c=>c.Sorting).ToList();
            var whereYouFromList = context.WhereYouFroms.Where(c=>c.IsDeleted == false || c.Id == guest.WhereYouId).OrderBy(c=>c.Sorting).ToList();
            var howYouKnowUsList = context.HowYouKnowUss.Where(c => c.IsDeleted == false || c.Id == guest.KnowUsId).OrderBy(c=>c.Sorting).ToList();
            if(ticketList.Count > 0  )
            {
               for(int i = 0; i < ticketList.Count; i++)
                {
                    var ss = new TicketModel
                    {
                        Id = ticketList[i].Id,
                        PriceForAdult = ticketList[i].PriceForAdult,
                        PriceForKids = ticketList[i].PriceForKids,
                        TicketName = ticketList[i].TicketName,
                        QuantityForAdult = 0,
                        QuantityForKids = 0,
                        SubTotalAdult = 0,
                        SubTotalKids = 0
                    };
                    ticketModels.Add(ss);
                }
               for(int i = 0; i < ticketModels.Count; i++)
                {
                    var index = guestTickets.IndexOf(guestTickets.Find(c => c.TicketId == ticketModels[i].Id));
                    if(index != -1)
                    {
                        ticketModels[i].QuantityForAdult = guestTickets[index].CountOfAdult;
                        ticketModels[i].QuantityForKids = guestTickets[index].CountOfKids;
                        ticketModels[i].SubTotalAdult = (decimal)(guestTickets[index].PriceForAdult * guestTickets[index].CountOfAdult);
                        ticketModels[i].SubTotalKids = (decimal)(guestTickets[index].PriceForKids * guestTickets[index].CountOfKids);
                        ticketModels[i].PriceForAdult = (decimal)guestTickets[index].PriceForAdult;
                        ticketModels[i].PriceForKids = (decimal)guestTickets[index].PriceForKids;

                    }
                }

            }
            var model = new EditTicketViewModel
            {
                Tickets = ticketModels,
                WhereYouFroms = whereYouFromList,
                DepositWays = depositWayList,
                HowYouKnowUs = howYouKnowUsList
            };
            return model;
        }    
        public async Task<Guest> EditGuest(Guest model)
        {
            var guest = context.Guests.FirstOrDefault(c => c.Id == model.Id);

            if(guest != null)
            {
                guest.DateOfDeposit = model.DateOfDeposit;
                guest.Deposit = model.Deposit;
                guest.DepositWayId = model.DepositWayId;
                guest.DiscountByAmount = model.DiscountByAmount;
                guest.DiscountByPercentage = model.DiscountByPercentage;
                guest.Email = model.Email;
                guest.GrandTotal = model.GrandTotal;
                guest.Identifier = model.Identifier;
                guest.IsCanceled = false;
                guest.KnowUsId = model.KnowUsId;
                guest.LeftToPay = model.LeftToPay;
                guest.Name = model.Name;
                guest.PaymentCash = model.PaymentCash;
                guest.PaymentVisa = model.PaymentVisa;
                guest.Phone = model.Phone;
                guest.TaxIncluded = model.TaxIncluded;
                guest.TotalCountOfguest = model.TotalCountOfguest;
                guest.WhereYouId = model.WhereYouId;
                guest.Id = model.Id;
                guest.DateOfBooking = model.DateOfBooking;
                guest.BookingTypeId = model.BookingTypeId;
                guest.DebitNote = model.DebitNote;
                context.Guests.Update(guest);
               await context.SaveChangesAsync();
            }
            return model;
        }

        public listGuestViewModel GetGuestDetailsById(int id)
        {
            listGuestViewModel model = new listGuestViewModel();
            var guest = context.Guests.FirstOrDefault(c => c.Id == id);
            model.Guests = guest;
            var bookedArea = context.BookedGuestAreas.Where(c => c.GuestId == id).ToList();
            model.GuestAreas = bookedArea;
            if(guest.BookingTypeId == 1)
            {
                var tickets = context.GuestTickets.Include(c=>c.Ticket).Where(c => c.GuestId == id).ToList();
                List<GuestTicketViewModel> guestTickets = new List<GuestTicketViewModel>();
                for(int i = 0; i < tickets.Count; i++)
                {
                    var tvm = new GuestTicketViewModel
                    {
                        TicketName = tickets[i].Ticket.TicketName,
                        CountOfAdult = tickets[i].CountOfAdult,
                        CountOfKids = tickets[i].CountOfKids,
                        PriceForAdult = (decimal)tickets[i].PriceForAdult,
                        PriceForKids = (decimal)tickets[i].PriceForKids,
                        SubTotal = (decimal)((tickets[i].CountOfAdult * tickets[i].PriceForAdult) +
                                 (tickets[i].CountOfKids * tickets[i].PriceForKids))
                    };
                    guestTickets.Add(tvm);
                }
                model.GuestTickets = guestTickets;
            }
            else
            {
                var activities = context.GuestActivities.Include(c => c.Activity).Where(c => c.GuestId == id).ToList();
                List<GuestActivityViewModel> gavm = new List<GuestActivityViewModel>();
                for (int k = 0; k < activities.Count; k++)
                {
                    var vm = new GuestActivityViewModel
                    {
                        ActivityName = activities[k].Activity.ActivityName,
                        Quantity = activities[k].Quantity,
                        IsIncluded = activities[k].IsIncluded,
                        Price = (decimal)activities[k].ActivityPrice
                    };
                    gavm.Add(vm);
                }

                model.GuestActivities = gavm;
            }
            return model;
        }
    }
}
