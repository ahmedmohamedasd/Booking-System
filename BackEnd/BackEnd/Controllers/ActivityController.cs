using AutoMapper;
using BackEnd.Dtos;
using BackEnd.Iservices;
using BackEnd.Models;
using BackEnd.Validations;
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
    public class ActivityController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IActivityRepository _activityRepository;
        public ActivityController(IActivityRepository activityRepository , IMapper mapper)
        {
            this._mapper = mapper;
            this._activityRepository = activityRepository;
        }
        // GET: api/<ActivityController>
        [HttpGet("ListOfActivities")]
        public List<ActivityDtos> Get()
        {
            var model = _activityRepository.GetListOfActivities();
            var modelDtos = _mapper.Map<List<ActivityDtos>>(model);
            Console.WriteLine(modelDtos);
            return modelDtos;
        }

        // GET api/<ActivityController>/5
        [HttpGet("GetActivityById/{id}")]
        public async Task<ActionResult<ActivityDtos>> GetActivityById(int id)
        {
            var activity = await _activityRepository.GetActivityById(id);
            if(activity != null)
            {
                var activityDtos = _mapper.Map<ActivityDtos>(activity);
                return activityDtos;
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<ActivityController>
        [HttpPost("AddActivity")]
        public async Task<ActionResult<ActivityDtos>> AddActitivy(ActivityDtos model)
        {
            try
            {
                var activity = _mapper.Map<Activity>(model);
                var validators = new ActivityValidation();
                var result = validators.Validate(activity);
                if (!result.IsValid)
                {
                    return BadRequest(result.Errors);
                }
                await _activityRepository.AddActivity(activity);
                return model;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error In Adding Activity");
            }
     
          
        }

        // PUT api/<ActivityController>/5
        [HttpPut("EditActivity/{id}")]
        public async Task<ActionResult<ActivityDtos>> EditActivity(int id , ActivityDtos model)
        {
            if(id != model.Id)
            {
                return BadRequest();
            }
            var activity = _mapper.Map<Activity>(model);
            var validators = new ActivityValidation();
            var result = validators.Validate(activity);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            await _activityRepository.EditActivity(activity);
            return model;
        }

        // DELETE api/<ActivityController>/5
        [HttpDelete("DeleteActivity/{id}")]
        public async Task<ActionResult> DeleteActivity(int id)
        {
            var activity = await _activityRepository.GetActivityById(id);
            if(activity != null)
            {
                await _activityRepository.DeleteActivity(id);

                return Ok();
            }
            else
            {
                return NotFound();
            }

        }

        [HttpDelete("UndoDeleteActivity/{id}")]
        public async Task<ActionResult> UndoDeleteActivity(int id)
        {
            var result = await _activityRepository.UndoDeleteActivity(id);
            if(result != null)
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
