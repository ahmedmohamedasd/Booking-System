using BackEnd.Iservices;
using BackEnd.Models;
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
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;
       
        public EventController(IEventRepository eventRepository ,
                                IGuestEventRepository guestEventRepository)
        {
            _eventRepository = eventRepository;
           
        }
        [HttpGet("GetListOfEvents")]
        public List<Event> GetListOfevent()
        {
            var dateOfNow = DateTime.UtcNow.Date;
            var model = _eventRepository.GetListOfEvents();
            return model;
        }
        [HttpPost("AddEvent")]
        public async Task<ActionResult> AddEvent(Event model)
        {
            try
            {
                await _eventRepository.AddEvent(model);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error In Adding event");
            }

        }
        [HttpGet("getEventById/{id}")]
        public async Task<ActionResult<Event>> GetEventById(int id)
        {
           var events =  await _eventRepository.GetEventById(id);
            return events;
        }
        [HttpPut("EditEvent/{id}")]
        public async Task<ActionResult> EditEvent(int id , Event model)
        {
            if(id != model.Id)
            {
                return BadRequest("Id Not Match");
            }
            try
            {
                await _eventRepository.EditEvent(model);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "error in Editing Event");
            }
        }
       [HttpDelete("DeleteEvent/{id}")]
       public async Task<ActionResult> DeleteEvent(int id)
        {
            try
            {
                var eventInDb = await _eventRepository.GetEventById(id);
                if (eventInDb == null)
                {
                    return NotFound("This Event not Found in Db");
                }

                await _eventRepository.DeleteEvent(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Deleteing this Event");
            }
           

        }
    }
}
