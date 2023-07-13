using AutoMapper;
using BackEnd.Dtos;
using BackEnd.Dtos.DtosViewModel;
using BackEnd.Iservices;
using BackEnd.Models;
using BackEnd.Validations;
using BackEnd.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class BookingController : ControllerBase
    {
        private readonly IGuestTicketRepository _guestTicketRepo;
        private readonly IBookingRepository bookingRepository;
        private readonly IGuestActivityRepository _guestActivityRp;
        private readonly IGuestRepository _guestRepository;
        private readonly IGuestAreaRepository _guestArea;
        private readonly IWhereYouFromRepository _placeRepository;
        private readonly IHowYouKnowUsRepository _socialRepository;
        private readonly IMapper _mapper;

        public BookingController(IBookingRepository _bookingRepository,
                                 IMapper _mapper,
                                 IGuestActivityRepository _guestActivityRp,
                                 IGuestTicketRepository guestTicket,
                                 IGuestRepository guestRepository,
                                 IGuestAreaRepository guestArea,
                                 IHowYouKnowUsRepository socialRepository ,
                                 IWhereYouFromRepository placeRepository)
        {
            this._mapper = _mapper;
            this.bookingRepository = _bookingRepository;
            this._guestActivityRp = _guestActivityRp;
            this._guestTicketRepo = guestTicket;
            this._guestRepository = guestRepository;
            this._guestArea = guestArea;
            _placeRepository = placeRepository;
            _socialRepository = socialRepository;
        }

        // GET: api/<BookingController>
        [HttpGet("{date}")]
        public IEnumerable<AreaViewModel> GetListOfAreas(string date)
        {
            DateTime dateTime13 = DateTime.ParseExact(date, "MM-dd-yyyy", null);
            return bookingRepository.GetBookedArea(dateTime13);
        }

        [HttpGet("listOfBookingTypes")]
        public IEnumerable<BookingType> GetListOfBookingtype()
        {
            var model = bookingRepository.GetListOfBookingType();
            return model;
        }
     
        [HttpGet("getListOfTicketViewModel")]
        public TicketViewModel GetListOfTicketViewModel()
        {
            var model = bookingRepository.GetTicketviewModel();
            return model;
        }
     
       
        [HttpPost("AddGuestTicket")]
        public async Task<ActionResult<AddGuestViewModel>> AddGuestWithTicketsAsync([FromBody] AddGuestViewModel model)
        {
            try
            {

                if (model.GuestInfo.WhereYouId == 0)
                {
                    if (model.GuestInfo.NewPlaceName != "")
                    {
                        var sorting = _placeRepository.GetSorting();
                        WhereYouFrom whereYou = new WhereYouFrom
                        {
                            IsDeleted = false,
                            PlaceName = model.GuestInfo.NewPlaceName,
                            Sorting = sorting
                        };
                        await _placeRepository.AddPlace(whereYou);
                        int placeId = _placeRepository.GetIdByName(model.GuestInfo.NewPlaceName);
                        model.GuestInfo.WhereYouId = placeId;
                    }
                    else
                    {
                        model.GuestInfo.WhereYouId = 1;
                    }
                }
                if (model.GuestInfo.KnowUsId == 0)
                {
                    if (model.GuestInfo.NewSocialName != "")
                    {
                        var sorting = _socialRepository.GetSorting();
                        HowYouKnowUs howYouKnow = new HowYouKnowUs
                        {
                            IsDeleted = false,
                            WayName = model.GuestInfo.NewSocialName,
                            Sorting = sorting
                        };
                        await _socialRepository.AddWay(howYouKnow);
                        int KnowusId = _socialRepository.GetIdByName(model.GuestInfo.NewSocialName);
                        model.GuestInfo.KnowUsId = KnowusId;
                    }
                    else
                    {
                        model.GuestInfo.KnowUsId = 1;
                    }
                }
                var guestModel = _mapper.Map<Guest>(model.GuestInfo);
                var validators = new GuestValidation();
                var result = validators.Validate(guestModel);
                if (!result.IsValid)
                {
                    return BadRequest(result.Errors);
                }

                //Guest guestModel = new Guest
                //{
                //    BookingTypeId = model.GuestInfo.BookingTypeId,
                //    DateOfBooking = model.GuestInfo.DateOfBooking,
                //    DateOfDeposit = model.GuestInfo.DateOfDeposit,
                //    DebitNote = model.GuestInfo.DebitNote,
                //    DepositWayId = model.GuestInfo.DepositWayId,
                //    DiscountByAmount = model.GuestInfo.DiscountByAmount,
                //    DiscountByPercentage = model.GuestInfo.DiscountByPercentage,
                //    Deposit = model.GuestInfo.Deposit,
                //    Email = model.GuestInfo.Email,
                //    GrandTotal = model.GuestInfo.GrandTotal,
                //    Name = model.GuestInfo.Name,
                //    IsCanceled = false,
                //    KnowUsId = model.GuestInfo.KnowUsId,
                //    WhereYouId = model.GuestInfo.WhereYouId,
                //    LeftToPay = model.GuestInfo.LeftToPay,
                //    Identifier = model.GuestInfo.Identifier,
                //    PaymentCash = model.GuestInfo.PaymentCash,
                //    Phone = model.GuestInfo.Phone,
                //    TaxIncluded = model.GuestInfo.TaxIncluded,
                //    PaymentVisa = model.GuestInfo.PaymentVisa,
                //    TotalCountOfguest = model.GuestInfo.TotalCountOfguest
                //};
                //  var guestModel = mapper.Map<Guest>(model.GuestInfo);
                var GuestId = bookingRepository.AddNewGuest(guestModel);
                List<BookedGuestArea> bookArea = new List<BookedGuestArea>();
                for (int i = 0; i < model.SelectedArea.Count; i++)
                {
                    var ss = new BookedGuestArea
                    {
                        AreaId = model.SelectedArea[i].AreaId,
                        DateOfBooking = model.SelectedArea[i].DateOfBooking,
                        GuestId = GuestId
                    };
                    bookArea.Add(ss);
                }
                await bookingRepository.AddBookedGuestArea(bookArea);
                List<GuestTicket> guestTickets = new List<GuestTicket>();
                for (int i = 0; i < model.ListOfTicket.Count; i++)
                {
                    var ss = new GuestTicket
                    {
                        TicketId = model.ListOfTicket[i].Id,
                        CountOfAdult = model.ListOfTicket[i].QuantityForAdult,
                        CountOfKids = model.ListOfTicket[i].QuantityForKids,
                        PriceForAdult = model.ListOfTicket[i].PriceForAdult,
                        PriceForKids = model.ListOfTicket[i].PriceForKids,
                        GuestId = GuestId,
                        DateOfBooking = model.GuestInfo.DateOfBooking
                    };
                    guestTickets.Add(ss);
                }
                await _guestTicketRepo.AddGuestTicket(guestTickets);
                return model;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Added Guest");

            }

        }

        [HttpPost("EditGuestTicket")]
        public async Task<ActionResult<AddGuestViewModel>> EditGuestWithTicket(AddGuestViewModel model)
        {
            try {

                if (model.GuestInfo.WhereYouId == 0)
                {
                    if (model.GuestInfo.NewPlaceName != "")
                    {
                                               
                        int KnowusId = bookingRepository.AddNewPlace(model.GuestInfo.NewPlaceName);
                        model.GuestInfo.WhereYouId = KnowusId;
                    }
                    else
                    {
                        model.GuestInfo.WhereYouId = 1;
                    }
                }
                if (model.GuestInfo.KnowUsId == 0)
                {
                    if (model.GuestInfo.NewSocialName != "")
                    {
                        int KnowusId = bookingRepository.AddNewSocialWay(model.GuestInfo.NewSocialName);
                        model.GuestInfo.KnowUsId = KnowusId;
                    }
                    else
                    {
                        model.GuestInfo.KnowUsId = 1;
                    }
                }
                var guestModel = _mapper.Map<Guest>(model.GuestInfo);
                guestModel.IsCanceled = false;

                var validators = new GuestValidation();
                var result = validators.Validate(guestModel);
                if (!result.IsValid)
                {
                    return BadRequest(result.Errors);
                }
              
                //  await bookingRepository.DeleteGuestTicket(model.GuestInfo.Id);
                List<GuestTicket> guestTickets = new List<GuestTicket>();
                for (int i = 0; i < model.ListOfTicket.Count; i++)
                {
                    var ss = new GuestTicket
                    {
                        TicketId = model.ListOfTicket[i].Id,
                        CountOfAdult = model.ListOfTicket[i].QuantityForAdult,
                        CountOfKids = model.ListOfTicket[i].QuantityForKids,
                        PriceForAdult = model.ListOfTicket[i].PriceForAdult,
                        PriceForKids = model.ListOfTicket[i].PriceForKids,
                        GuestId = model.GuestInfo.Id
                    };
                    guestTickets.Add(ss);
                }
                var guestTicketsInDb = _guestTicketRepo.GetListOfGuestTickets(guestModel.Id);
                if (guestTicketsInDb.Count > 0)
                {
                    for (int i = 0; i < guestTicketsInDb.Count; i++)
                    {
                        var index = guestTickets.IndexOf(guestTickets.Find(c => c.TicketId == guestTicketsInDb[i].TicketId));
                        if (index != -1)
                        {
                            guestTicketsInDb[i].TicketId = guestTickets[index].TicketId;
                            guestTicketsInDb[i].PriceForAdult = guestTickets[index].PriceForAdult;
                            guestTicketsInDb[i].PriceForKids = guestTickets[index].PriceForKids;
                            guestTicketsInDb[i].CountOfAdult = guestTickets[index].CountOfAdult;
                            guestTicketsInDb[i].CountOfKids = guestTickets[index].CountOfKids;
                            await _guestTicketRepo.EditGuestTickets(guestModel.Id, guestTicketsInDb[i]);
                            guestTickets.Remove(guestTickets[index]);
                        }
                        else
                        {
                            await _guestTicketRepo.DeleteGuestTickets(guestModel.Id, guestTicketsInDb[i].TicketId);
                        }
                    }
                }
                await _guestTicketRepo.AddGuestTicket(guestTickets);
                await bookingRepository.EditGuest(guestModel);
                return model;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Added Guest");

            }
        }

        [HttpGet("getlistofguests/{date}")]
        public IEnumerable<listGuestViewModel> GetListOfGuests(string date)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime dateAfterConverting = DateTime.ParseExact(date, "MM-dd-yyyy", provider);
            var test = dateAfterConverting;
            var model = bookingRepository.GetListOfGuests(dateAfterConverting);
            return model;

        }

        [HttpPost("EditSelectedViewModel")]
        public IEnumerable<AreaViewModel> getSelectedArea(EditSelectedAreaViewModel model)
        {
            var selectedAreas = bookingRepository.GetSelectedBookedArea(model);
            return selectedAreas;

        }

        [HttpPost("EditGuestArea")]
        public async Task<IEnumerable<BookedGuestArea>> EditGuestArea(EditGuestAreaViewModel model)
        {
            var guestId = model.OldBooking[0].GuestId;
            List<BookedGuestArea> NewModel = new List<BookedGuestArea>();

            if (model.NewBooking[0].DateOfBooking.Date == model.OldBooking[0].DateOfBooking.Date)
            {

                for (int i = 0; i < model.NewBooking.Count; i++)
                {
                    var ss = new BookedGuestArea
                    {
                        AreaId = model.NewBooking[i].AreaId,
                        DateOfBooking = model.NewBooking[i].DateOfBooking,
                        GuestId = guestId
                    };
                    NewModel.Add(ss);
                }

                await _guestArea.EditGuestArea(guestId, NewModel);
                return NewModel;
            }
            else
            {
                var guest = await bookingRepository.EditGuest(guestId, model.NewBooking[0].DateOfBooking);
                var guestArea = _guestArea.GetAllGuestAreas(guestId);
                if (guestArea.Count > 0)
                {
                    await _guestArea.DeleteGuestArea(guestArea);
                }
                // await bookingRepository.DeleteGuestArea(model.OldBooking);
                for (int i = 0; i < model.NewBooking.Count; i++)
                {
                    var ss = new BookedGuestArea
                    {
                        AreaId = model.NewBooking[i].AreaId,
                        DateOfBooking = model.NewBooking[i].DateOfBooking,
                        GuestId = guestId
                    };
                    NewModel.Add(ss);
                }
                await _guestArea.AddGuestArea(NewModel);
                if (guest.BookingTypeId == 1)
                {
                    await _guestTicketRepo.EditGuestTicketsDateBooking(guestId, model.NewBooking[0].DateOfBooking);
                }
                if (guest.BookingTypeId != 1)
                {
                    await _guestActivityRp.EditGuestActivityDateBooking(guestId, model.NewBooking[0].DateOfBooking);

                }
                return NewModel;
            }

        }

        [HttpGet("GetEditTicketViewModel/{guestId}")]
        public EditTicketViewModel GetEditTicketViewModel(int guestId)
        {
            var model = bookingRepository.GetEditTicketViewModel(guestId);
            return model;
        }
       
        [HttpPut("cancelGuestBooking/{guestId}")]
        public async Task<ActionResult<int>> CancelGuestBooking(int guestId)
        {
            await _guestArea.DeleteGuestArea(guestId);
            var result = await _guestRepository.CancelGuest(guestId);
            if (result == true)
            {
                return guestId;
            }
            return StatusCode(StatusCodes.Status404NotFound, "Guest Not Found");
        }
       
        [HttpPut("undoCancelGuestBooking/{guestId}")]
        public async Task<ActionResult<int>> UndoCancelGuestBooking(int guestId , List<SelectedArea> model)
        {
            List<BookedGuestArea> bookedGuests = new List<BookedGuestArea>();
            for(int i = 0; i < model.Count; i++)
            {
                var ss = new BookedGuestArea
                {
                    AreaId = model[i].AreaId,
                    DateOfBooking = model[i].DateOfBooking,
                    GuestId = guestId
                };
                bookedGuests.Add(ss);
            }
            await _guestArea.AddGuestArea(bookedGuests);
            var result = await _guestRepository.UndoCancelGuest(guestId , model[0].DateOfBooking);
            if (result == true)
            {
                return guestId;
            }
            return StatusCode(StatusCodes.Status404NotFound, "Guest Not Found");
        }

        [HttpDelete("DeleteGuestBooking/{guestId}")]
        public async Task<ActionResult<int>> DeleteGuestBooking(int guestId)
        {
            var guestInDb = _guestRepository.GetGuestById(guestId);
            if (guestInDb != null)
            {
                await _guestRepository.DeleteGuest(guestId);
                if (guestInDb.BookingTypeId == 1)
                {
                    await _guestTicketRepo.DeleteGuestTickets(guestId);
                }
                else
                {
                    await _guestActivityRp.DeleteGuestActivity(guestId);
                }
                var guestArea = _guestArea.GetAllGuestAreas(guestId);
                if (guestArea.Count > 0)
                {
                    await _guestArea.DeleteGuestArea(guestArea);
                }
            }
            return guestId;

        }
        
        [HttpGet("GetGuestWithPhoneAndDate/{dateOfFrom}/{dateOfTo}/{phone}")]
        public IEnumerable<GuestDtos> GetGuestWithPhone(string dateOfFrom, string dateOfTo , string phone)
        {
            DateTime DateFrom = DateTime.ParseExact(dateOfFrom, "MM-dd-yyyy", null);
            DateTime DateTo = DateTime.ParseExact(dateOfTo, "MM-dd-yyyy", null);
            var model = _guestRepository.GetGuestWithPhoneNumber(DateFrom, DateTo, phone);
            return model;
        }
        
        [HttpGet("GetGuestWithNameAndDate/{dateOfFrom}/{dateOfTo}/{name}")]
        public IEnumerable<GuestDtos> GetGuestWithName(string dateOfFrom, string dateOfTo, string name)
        {
            DateTime DateFrom = DateTime.ParseExact(dateOfFrom, "MM-dd-yyyy", null);
            DateTime DateTo = DateTime.ParseExact(dateOfTo, "MM-dd-yyyy", null);
            var model = _guestRepository.GetGuestWithName(DateFrom, DateTo, name);
            return model;
        }
      
        [HttpGet("GetGuestWithNameOrPhone/{typeOfSearch}/{valueOfSearch}")]
        public IEnumerable<GuestDtos> GetGuestWithName(int typeOfSearch, string valueOfSearch)
        {
           if(typeOfSearch == 1)
            {
                var model = _guestRepository.GetGuestWithName(valueOfSearch);
                return model;
            }
            else
            {
                var model = _guestRepository.GetGuestWithPhoneNumber(valueOfSearch);
                return model;
            }
           
        }
       [HttpGet("GetGuestDetailsById/{id}")]
        public listGuestViewModel GetGuestDetailsById(int id)
        {
            var model = bookingRepository.GetGuestDetailsById(id);
            return model;
        }
        [HttpGet("filterAddGuestWithPhone/{phone}")]
        public GuestDtos filterAddGuestWithPhone(string phone)
        {
            var model = _guestRepository.getGuestByPhone(phone);
            return model;
        }
    }
}
