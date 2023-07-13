using BackEnd.Iservices;
using BackEnd.Models;
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
    public class DepositWayController : ControllerBase
    {

        private readonly IDepositWayesRepository _depositWayes;
        public DepositWayController(IDepositWayesRepository depositWayes)
        {
            this._depositWayes = depositWayes;
        }
        // GET: api/<ActivityController>
        [HttpGet("ListOfWays")]
        public List<DepositWay> Get()
        {
            var model = _depositWayes.GetListOfWays();
            return model;
        }

        // GET api/<ActivityController>/5
        [HttpGet("GetWayById/{id}")]
        public async Task<ActionResult<DepositWay>> GetWayById(int id)
        {
            var Way = await _depositWayes.GetWayById(id);
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
        public async Task<ActionResult<DepositWay>> AddWay(DepositWay model)
        {
            try
            {
                await _depositWayes.AddWay(model);
                return model;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error In Adding Activity");
            }


        }

        // PUT api/<ActivityController>/5
        [HttpPut("EditWay/{id}")]
        public async Task<ActionResult<DepositWay>> EditWay(int id, DepositWay model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }
            var result = await _depositWayes.EditWay(model);
            return model;
        }

        // DELETE api/<ActivityController>/5
        [HttpDelete("DeleteWay/{id}")]
        public async Task<ActionResult> DeleteWay(int id)
        {
            var Way = await _depositWayes.GetWayById(id);
            if (Way != null)
            {
                await _depositWayes.DeleteWay(id);

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
            var result = await _depositWayes.UndoDeleteWay(id);
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
