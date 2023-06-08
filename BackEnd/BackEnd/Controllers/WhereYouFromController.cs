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
    public class WhereYouFromController : ControllerBase
    {
        private readonly IWhereYouFromRepository _whereYouFromRepository;
        public WhereYouFromController(IWhereYouFromRepository whereYouFromRepository)
        {
            this._whereYouFromRepository = whereYouFromRepository;
        }
        // GET: api/<ActivityController>
        [HttpGet("ListOfPlaces")]
        public List<WhereYouFrom> Get()
        {
            var model = _whereYouFromRepository.GetListOfPlaces();
            return model;
        }

        // GET api/<ActivityController>/5
        [HttpGet("GetPlaceById/{id}")]
        public async Task<ActionResult<WhereYouFrom>> GetPlaceById(int id)
        {
            var Place = await _whereYouFromRepository.GetPlaceById(id);
            if (Place != null)
            {
                return Place;
            }
            else
            {
                return NotFound();
            }
        }

       
        [HttpPost("AddPlace")]
        public async Task<ActionResult<WhereYouFrom>> AddPlace(WhereYouFrom model)
        {
            try
            {
                await _whereYouFromRepository.AddPlace(model);
                return model;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error In Adding Place");
            }

        }
        
        [HttpPut("EditPlace/{id}")]
        public async Task<ActionResult<WhereYouFrom>> EditPlace(int id, WhereYouFrom model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }
            var result = await _whereYouFromRepository.EditPlace(model);
            return model;
        }

        [HttpDelete("DeletePlace/{id}")]
        public async Task<ActionResult> DeletePlace(int id)
        {
            var Place = await _whereYouFromRepository.GetPlaceById(id);
            if (Place != null)
            {
                await _whereYouFromRepository.DeletePlace(id);

                return Ok();
            }
            else
            {
                return NotFound();
            }

        }

        [HttpDelete("UndoDeletePlace/{id}")]
        public async Task<ActionResult> UndoDeletePlace(int id)
        {
            var result = await _whereYouFromRepository.UndoDeletePlace(id);
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
