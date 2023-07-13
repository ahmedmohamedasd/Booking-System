using AutoMapper;
using BackEnd.Dtos;
using BackEnd.Dtos.DtosViewModel;
using BackEnd.Iservices;
using BackEnd.Models;
using BackEnd.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class BookingByActivitiesController : ControllerBase
    {
        private readonly IBookingByActivityRepository _Activity;
        private readonly IBookingRepository bookingRepository;
        private readonly IGuestActivityRepository _guestActivityRp;
        private readonly IGuestRepository _guestRepository;
        private readonly IMapper _mapper;
        public BookingByActivitiesController(IBookingByActivityRepository bookingByActivity ,
                                             IBookingRepository _bookingRepository ,
                                             IGuestActivityRepository guestActivityRp,
                                             IGuestRepository guestRepository,
                                             IMapper maper)
        {
            this._Activity = bookingByActivity;
            this.bookingRepository = _bookingRepository;
            this._guestActivityRp = guestActivityRp;
            _guestRepository = guestRepository;
            _mapper = maper;
        }
        // GET: api/<BookingByActivitiesController>
        [HttpGet]
        [HttpGet("getActivityViewModel")]
        public ActivityViewModel GetActivityViewModel()
        {
            var model = _Activity.GetActivityViewModel();
            return model;
        }

        // GET api/<BookingByActivitiesController>/5
        [HttpGet("GetEditActivityViewModel/{guestId}")]
        public EditActivityViewModel Get(int guestId)
        {
            var model = _Activity.GetEditActivityViewModel(guestId);
            return model;
        }
        [HttpGet("GetGuestWithName/{name}")]
        public GuestDtos GetGuestByName(string name)
        {
            var guestInfo = _guestRepository.getGuestByName(name);
            return guestInfo;
        }
        // POST api/<BookingByActivitiesController>
        [HttpPost("postActivityViewModel")]
        public async Task<ActionResult<AddActivityViewModelDtos>> PostGuestWithActivities(AddActivityViewModelDtos model)
        {
            try
            {

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
                //Guest guestModel = new Guest
                //{
                //    Id = model.GuestInfo.Id,
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
                List<GuestActivity> guestActivities = new List<GuestActivity>();
                for(int i = 0; i < model.SelectedActivities.Count; i++)
                {
                    var ss = new GuestActivity
                    {
                        ActivityId = model.SelectedActivities[i].Id,
                        ActivityPrice = model.SelectedActivities[i].ActivityPrice,
                        GuestId = GuestId,
                        DateOfBooking = guestModel.DateOfBooking,
                        IsIncluded = model.SelectedActivities[i].IsIncluded,
                        Quantity = model.SelectedActivities[i].Quantity,
                        SubTotal = model.SelectedActivities[i].SubTotal

                    };
                    guestActivities.Add(ss);
                }

                await _guestActivityRp.AddGuestActivity(guestActivities);

                return  model;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Added Guest");

            }
            
        }

        // DELETE api/<BookingByActivitiesController>/5
        [HttpPost("EditGuestWithActivity")]
        public async Task<ActionResult<AddActivityViewModelDtos>> EditGuestWithActivity(AddActivityViewModelDtos model)
        {
            try
            {
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
                //Guest guestModel = new Guest
                //{
                //    Id = model.GuestInfo.Id,
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
                await  bookingRepository.EditGuest(guestModel);
                List<GuestActivity> guestActivities = new List<GuestActivity>();
                for (int i = 0; i < model.SelectedActivities.Count; i++)
                {
                    var ss = new GuestActivity
                    {
                        ActivityId = model.SelectedActivities[i].Id,
                        ActivityPrice = model.SelectedActivities[i].ActivityPrice,
                        GuestId = model.GuestInfo.Id,
                        DateOfBooking = guestModel.DateOfBooking,
                        IsIncluded = model.SelectedActivities[i].IsIncluded,
                        Quantity = model.SelectedActivities[i].Quantity,
                        SubTotal = model.SelectedActivities[i].SubTotal

                    };
                    guestActivities.Add(ss);
                }
                var activityinDb = _guestActivityRp.GetListOfGuestActivities(guestModel.Id);
                if(activityinDb.Count > 0)
                {
                    for(int i = 0; i < activityinDb.Count; i++)
                    {
                        var index = guestActivities.IndexOf(guestActivities.Find(c => c.ActivityId == activityinDb[i].ActivityId));
                        if(index != -1)
                        {
                            activityinDb[i].Quantity = guestActivities[index].Quantity;
                            activityinDb[i].IsIncluded = guestActivities[index].IsIncluded;
                            activityinDb[i].SubTotal = guestActivities[index].SubTotal;
                           await _guestActivityRp.EditGuestActivity(guestModel.Id, activityinDb[i]);
                            guestActivities.Remove(guestActivities[index]);
                        }
                        else
                        {
                            await _guestActivityRp.DeleteGuestActivity(guestModel.Id, activityinDb[i].ActivityId);
                        }
                    }
                }

                await _guestActivityRp.AddGuestActivity(guestActivities);

                return model;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Added Guest");

            }
            
        }
    }
}
