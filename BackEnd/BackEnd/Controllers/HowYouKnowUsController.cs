using AutoMapper;
using BackEnd.Iservices;
using BackEnd.Models;
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
    public class HowYouKnowUsController : ControllerBase
    {
      
        private readonly IHowYouKnowUsRepository _howYouKnowUsRepository;
        public HowYouKnowUsController(IHowYouKnowUsRepository howYouKnowUsRepository)
        {
            this._howYouKnowUsRepository = howYouKnowUsRepository;
        }
        // GET: api/<ActivityController>
        [HttpGet("ListOfWays")]
        public List<HowYouKnowUs> Get()
        {
            var model = _howYouKnowUsRepository.GetListOfWays();
            return model;
        }

        // GET api/<ActivityController>/5
        [HttpGet("GetWayById/{id}")]
        public async Task<ActionResult<HowYouKnowUs>> GetWayById(int id)
        {
            var Way = await _howYouKnowUsRepository.GetWayById(id);
            if (Way != null)
            {
                
                return Way;
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<ActivityController>
        [HttpPost("AddWay")]
        public async Task<ActionResult<HowYouKnowUs>> AddWay(HowYouKnowUs model)
        {
            try
            {
                await _howYouKnowUsRepository.AddWay(model);
                return model;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error In Adding Activity");
            }


        }

        // PUT api/<ActivityController>/5
        [HttpPut("EditWay/{id}")]
        public async Task<ActionResult<HowYouKnowUs>> EditWay(int id, HowYouKnowUs model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }
            var result = await _howYouKnowUsRepository.EditWay(model);
            return model;
        }

        // DELETE api/<ActivityController>/5
        [HttpDelete("DeleteWay/{id}")]
        public async Task<ActionResult> DeleteWay(int id)
        {
            var Way = await _howYouKnowUsRepository.GetWayById(id);
            if (Way != null)
            {
                await _howYouKnowUsRepository.DeleteWay(id);

                return Ok();
            }
            else
            {
                return NotFound();
            }

        }

        [HttpDelete("UndoDeleteWay/{id}")]
        public async Task<ActionResult> UndoDeleteWay(int id)
        {
            var result = await _howYouKnowUsRepository.UndoDeleteWay(id);
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
