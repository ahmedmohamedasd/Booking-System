using AutoMapper;
using BackEnd.Dtos;
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
    public class TicketController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITicketRepository _ticketRepository;
        public TicketController(ITicketRepository ticketRepository , IMapper mapper)
        {
            this._ticketRepository = ticketRepository;
            this._mapper = mapper;
        }
   
        [HttpGet("ListOfTickets")]
        public List<TicketDtos> GetLitOfTickets()
        {
            var model = _ticketRepository.GetListOfTickets();
            var modelDtos = _mapper.Map<List<TicketDtos>>(model);
           
            return modelDtos;
        }

      
        [HttpGet("GetTicketById/{id}")]
        public async Task<ActionResult<TicketDtos>> GetTicketById(int id)
        {
            var Ticket = await _ticketRepository.GetTicketById(id);
            if (Ticket != null)
            {
                var activityDtos = _mapper.Map<TicketDtos>(Ticket);
                return activityDtos;
            }
            else
            {
                return NotFound();
            }
        }

  
        [HttpPost("AddTicket")]
        public async Task<ActionResult<TicketDtos>> AddTicket(TicketDtos model)
        {
            try
            {
                var ticket = _mapper.Map<Ticket>(model);
                await _ticketRepository.AddTicket(ticket);
                return model;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error In Adding Activity");
            }


        }

      
        [HttpPut("EditTicket/{id}")]
        public async Task<ActionResult<TicketDtos>> EditTicket(int id, TicketDtos model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }
            var ticket = _mapper.Map<Ticket>(model);
            var result = await _ticketRepository.EditTicket(ticket);
            return model;
        }

       
        [HttpDelete("DeleteTicket/{id}")]
        public async Task<ActionResult> DeleteTicket(int id)
        {
            var ticket = await _ticketRepository.GetTicketById(id);
            if (ticket != null)
            {
                await _ticketRepository.DeleteTicket(id);

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpDelete("UndoDeleteTicket/{id}")]
        public async Task<ActionResult> UndoDeleteTicket(int id)
        {
            var result = await _ticketRepository.UndoDeleteTicket(id);
            if (result != null)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }

        }
    }
}
