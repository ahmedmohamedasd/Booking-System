using AutoMapper;
using BackEnd.Dtos;
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
    public class CloseAreaController : ControllerBase
    {
        private readonly ICloseAreaRepository _closeAreaRepository;
        private readonly IMapper mapper;
        public CloseAreaController(ICloseAreaRepository _closeAreaRepository , IMapper _mapper)
        {
            this._closeAreaRepository = _closeAreaRepository;
            this.mapper = _mapper;
        }
        // GET: api/<CloseAreaController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CloseAreaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
       
        [HttpPost]
        public async Task<ActionResult<BlockArea[]>> Post([FromBody] BlockAreaDto[] model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                var blockArea = mapper.Map<BlockArea[]>(model);
                var result = await _closeAreaRepository.AddClosedArea(blockArea);

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Added New Area");
            }
        }

        // PUT api/<CloseAreaController>/5
        [HttpDelete("UndoClosedArea/{DateOfBlocked}")]
        public async Task<ActionResult> UndoClosedArea(string DateOfBlocked)
        {
            try
            {
                DateTime currentDate = DateTime.ParseExact(DateOfBlocked, "MM-dd-yyyy", null);

                 await _closeAreaRepository.DeleteClosedArea(currentDate);
                return StatusCode(StatusCodes.Status204NoContent, "Items Deleted Correctly");
            }
            catch (Exception)
            {
             return StatusCode(StatusCodes.Status500InternalServerError, "Error In Delete Blocked Areas");
            }


        }

        // DELETE api/<CloseAreaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
