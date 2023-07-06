using BackEnd.Data;
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
    public class AreasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IAreaRepository areaRepository;
        public AreasController(ApplicationDbContext _context , IAreaRepository _areaRepository)
        {
            this.context = _context;
            this.areaRepository = _areaRepository;
        }
        // GET: api/<AreasController>
        [HttpGet]
        public IEnumerable<Area> Get()
        {
            
            return areaRepository.GetAllAreas();
        }

        // GET api/<AreasController>/5
        [HttpGet("{id}")]
        public ActionResult<Area> Get(int id)
        {
            var result = context.Areas.FirstOrDefault(c => c.Id == id);
            if (result == null)
                return BadRequest();
            else
            {
                return result;
            }
            
        }

        // POST api/<AreasController>
        [HttpPost]
        public async Task<ActionResult<Area>> PostArea([FromBody] Area model)
        {
            try
            {
                if (model == null)
                    return BadRequest();
                var createModel = await areaRepository.AddArea(model);
                return createModel;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Added New Area");
            }
            
        }

        // PUT api/<AreasController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Area>> UpdateArea(int id,  Area model)
        {
            try
            {
               if(id != model.Id)
                {
                    return BadRequest("Area Name  doesn't Match");
                }
                else
                {
                    var areaInDb = await areaRepository.GetAreaById(id);
                    if (areaInDb == null)
                        return NotFound($"Area Not Found");

                    return await areaRepository.UpdateArea(model);
                }
               
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Updating Data");
            }
           
        }

        // DELETE api/<AreasController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Area>> Delete(int id)
        {
            try
            {
                var areaInDb =  areaRepository.GetAreaById(id);
                if (areaInDb == null)
                {
                    return NotFound("Area Not Found");
                }
                return await areaRepository.DeleteArea(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error In deleting Data");
            }
            
           
        }
    }
}
