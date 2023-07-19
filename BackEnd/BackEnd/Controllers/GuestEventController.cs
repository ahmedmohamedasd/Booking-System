using AutoMapper;
using BackEnd.Dtos;
using BackEnd.Iservices;
using BackEnd.Models;
using BackEnd.Validations;
using BackEnd.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestEventController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGuestEventRepository _guestEventRepository;
        private readonly IWhereYouFromRepository _placeRepository;
        private readonly IHowYouKnowUsRepository _socialRepository;
        public GuestEventController(IGuestEventRepository guestEventRepository ,
                                    IWhereYouFromRepository placeRepository,
                                    IHowYouKnowUsRepository socialRepository,
                                    IMapper mapper)
        {
            _placeRepository = placeRepository;
            _socialRepository = socialRepository;
            _mapper = mapper;
            _guestEventRepository = guestEventRepository;
        }
        [HttpGet("GetGuestEventViewModel")]
        public AddGuestEventViewModel GetGuestEventViewModel()
        {
            var model = _guestEventRepository.GetGuestEventViewModel();
            return model;
        }
        [HttpPost("AddGuestEvent")]
        public async Task<ActionResult<GuestEvent>> AddGuestEvent(GuestEventDtos model)
        {
            try
            {
                if (model.WhereYouId == 0)
                {
                    if (model.NewPlaceName != "")
                    {
                        var sorting = _placeRepository.GetSorting();
                        WhereYouFrom whereYou = new WhereYouFrom
                        {
                            IsDeleted = false,
                            PlaceName = model.NewPlaceName,
                            Sorting = sorting
                        };
                        var placeValidator = new WhereYouFromValidations();
                        var placeResult = placeValidator.Validate(whereYou);
                        if (!placeResult.IsValid)
                        {
                            return BadRequest(placeResult.Errors);
                        }
                        await _placeRepository.AddPlace(whereYou);
                        int placeId = _placeRepository.GetIdByName(model.NewPlaceName);
                        model.WhereYouId = placeId;
                    }
                    else
                    {
                        model.WhereYouId = 1;
                    }
                }
                if (model.KnowUsId == 0)
                {
                    if (model.NewSocialName != "")
                    {
                        var sorting = _socialRepository.GetSorting();
                        HowYouKnowUs howYouKnow = new HowYouKnowUs
                        {
                            IsDeleted = false,
                            WayName = model.NewSocialName,
                            Sorting = sorting
                        };
                        var WayValidator = new HowYouKnowUsValidation();
                        var wayResult = WayValidator.Validate(howYouKnow);
                        if (!wayResult.IsValid)
                        {
                            return BadRequest(wayResult.Errors);
                        }
                        await _socialRepository.AddWay(howYouKnow);
                        int KnowusId = _socialRepository.GetIdByName(model.NewSocialName);
                        model.KnowUsId = KnowusId;
                    }
                    else
                    {
                        model.KnowUsId = 1;
                    }
                }
                var gem = _mapper.Map<GuestEvent>(model);
                var validators = new GuestEventValidation();
                var result = validators.Validate(gem);
                if (result.IsValid)
                {
                   await _guestEventRepository.AddguestEvent(gem);
                    return Ok();
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"Error in Adding guest Event");
            }
           
        }

        [HttpGet("GetListOfGuest/{dateOfEvent}")]
        public IEnumerable<GuestEvent> GetListOfGuestEvents(string dateOfEvent)
        {
            DateTime DateFrom = DateTime.ParseExact(dateOfEvent, "dd-MM-yyyy", null);
            var model =  _guestEventRepository.GetListOfGuestEvent(DateFrom);
            return model;
        }
        [HttpGet("GetEditGuestEvent/{id}")]
        public async Task<ActionResult<EditGuestEventViewModel>> GetEditGuesteventViewModel(int id)
        {

            var guestEvent = await _guestEventRepository.GetGuestEventById(id);
            if(guestEvent == null)
            {
                return NotFound();
            }
            var model = _guestEventRepository.GetEditGuestEventViewModel(id);
            return model;
        }
        [HttpPut("EditGuestEvent/{id}")]
        public async Task<ActionResult> EditGuestEvent(int id , GuestEventDtos model)
        {
            try
            {
                if (id != model.Id)
                {
                    return BadRequest("ID Not Match");
                }
                if (model.WhereYouId == 0)
                {
                    if (model.NewPlaceName != "")
                    {
                        var sorting = _placeRepository.GetSorting();
                        WhereYouFrom whereYou = new WhereYouFrom
                        {
                            IsDeleted = false,
                            PlaceName = model.NewPlaceName,
                            Sorting = sorting
                        };
                        var placeValidator = new WhereYouFromValidations();
                        var placeResult = placeValidator.Validate(whereYou);
                        if (!placeResult.IsValid)
                        {
                            return BadRequest(placeResult.Errors);
                        }
                        await _placeRepository.AddPlace(whereYou);
                        int placeId = _placeRepository.GetIdByName(model.NewPlaceName);
                        model.WhereYouId = placeId;
                    }
                    else
                    {
                        model.WhereYouId = 1;
                    }
                }
                if (model.KnowUsId == 0)
                {
                    if (model.NewSocialName != "")
                    {
                        var sorting = _socialRepository.GetSorting();
                        HowYouKnowUs howYouKnow = new HowYouKnowUs
                        {
                            IsDeleted = false,
                            WayName = model.NewSocialName,
                            Sorting = sorting
                        };
                        var WayValidator = new HowYouKnowUsValidation();
                        var wayResult = WayValidator.Validate(howYouKnow);
                        if (!wayResult.IsValid)
                        {
                            return BadRequest(wayResult.Errors);
                        }
                        await _socialRepository.AddWay(howYouKnow);
                        int KnowusId = _socialRepository.GetIdByName(model.NewSocialName);
                        model.KnowUsId = KnowusId;
                    }
                    else
                    {
                        model.KnowUsId = 1;
                    }
                }
                var gem = _mapper.Map<GuestEvent>(model);
                var validators = new GuestEventValidation();
                var result = validators.Validate(gem);
                if (result.IsValid)
                {
                    var guestEventInDb = await _guestEventRepository.GetGuestEventById(id);
                    if (guestEventInDb == null)
                    {
                        return NotFound("Guest Not Found");
                    }
                    await _guestEventRepository.EditGuestevent(gem);
                    return Ok();
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Adding guest Event");
            }

        }
        
        [HttpPost("CancelGuestEvent/{id}")]
        public async Task<ActionResult> CancelGuestEvent(int id)
        {
            try
            {
                var guestEventInDb = await _guestEventRepository.GetGuestEventById(id);
                if (guestEventInDb == null)
                {
                    return NotFound("This Guest Not Found");
                }

                await _guestEventRepository.CancelGuestEvent(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Cancel Guest Order");
            }
             
        }
        [HttpPost("UndoCancelGuestEvent/{id}")]
        public async Task<ActionResult> UndoCancelGuestEvent(int id)
        {
            try
            {
                var guestEventInDb = await _guestEventRepository.GetGuestEventById(id);
                if (guestEventInDb == null)
                {
                    return NotFound("This Guest Not Found");
                }

                await _guestEventRepository.UndoCancelGuestEvent(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Undo Cancel Guest Order");
            }

        }
        [HttpDelete("DeleteGuestEvent/{id}")]
        public async Task<ActionResult> DeleteGuestEvent(int id)
        {
            try
            {
                var guestEventInDb = await _guestEventRepository.GetGuestEventById(id);
                if (guestEventInDb == null)
                {
                    return NotFound("This Guest Not Found");
                }

                await _guestEventRepository.DeleteGuestEvent(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Deleting guest");
            }

        }

        [HttpGet("GetListOfGuestByEventId/{id}")]
        public List<GuestEvent> GetListOfGuestByEventId(int id)
        {
            var model = _guestEventRepository.GetGuestEventByEventId(id);
            return model;
        }
    }
}
