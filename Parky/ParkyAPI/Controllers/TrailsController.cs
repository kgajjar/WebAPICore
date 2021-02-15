using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Models;
using ParkyAPI.Models.Dtos;
using ParkyAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Controllers
{
#pragma warning disable CS1591
    //[Route("api/Trails")]//Prefer usign static name rather than [Controller]
    [Route("api/v{version:apiVersion}/trails")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "ParkyOpenAPISpecTrails")]//Here we add the API calls to dropdown. (Bundling them)
    [ProducesResponseType(StatusCodes.Status400BadRequest)]//This is applicable to any method
    public class TrailsController : ControllerBase////Since Controller class derives from ControllerBase. We use Controller base as this isnt using views like a MVC app does.
    {
        //Get repo using DI
        private ITrailRepository _npRepo;
        private readonly IMapper _mapper;

        public TrailsController(ITrailRepository npRepo, IMapper mapper)
        {
            _npRepo = npRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of Trails
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<TrailDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetTrails()
        {
            var objList = _npRepo.GetTrails();

            //Obj to hold Dto model
            var objDto = new List<TrailDto>();

            foreach (var obj in objList)
            {
                /*
                 1) Convert output to - TrailDto
                 2) Source Object - obj
                 */
                objDto.Add(_mapper.Map<TrailDto>(obj));
            }
            //This returns Status Code: 200 meaning OK
            return Ok(objDto);
        }

        //To avoid ambiguos error you have to specify param in Http Get below to differentiate between 2 methods with same name
        //We also specify that param is integer
        /// <summary>
        /// Get individual Trails
        /// </summary>
        /// <param name="trailId">The Id of the Trail</param>
        /// <returns></returns>
        [HttpGet("{trailId:int}", Name = "GetTrail")]
        [ProducesResponseType(200, Type = typeof(TrailDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]//This enables us to show all responses in Documentation
        [ProducesDefaultResponseType]//Any error that is not one of the above
        [Authorize(Roles = "Admin")]//Only admin can access this
        public IActionResult GetTrail(int trailId)
        {
            var obj = _npRepo.GetTrail(trailId);
            if (obj == null)
            {
                return NotFound();
            }
            /*
                 1) Convert output to - TrailDto
                 2) Source Object - obj
            */
            var objDto = _mapper.Map<TrailDto>(obj);

            //This returns Status Code: 200 meaning OK
            return Ok(objDto);
        }

        [HttpGet("[action]/{nationalParkId:int}")]
        [ProducesResponseType(200, Type = typeof(TrailDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]//This enables us to show all responses in Documentation
        [ProducesDefaultResponseType]//Any error that is not one of the above
        public IActionResult GetTrailsInNationalPark(int nationalParkId)
        {
            var objList = _npRepo.GetTrailsInNationalPark(nationalParkId);
            if (objList == null)
            {
                return NotFound();
            }
            /*
                 1) Convert output to - TrailDto
                 2) Source Object - obj
            */
            var objDto = new List<TrailDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<TrailDto>(obj));
            }

            //This returns Status Code: 200 meaning OK
            return Ok(objDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(TrailDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]//This enables us to show all responses in Documentation
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateTrail([FromBody] TrailCreateDto trailDto)//We specify what we will get from the Body
        {
            if (trailDto == null)
            {
                return BadRequest(ModelState);//ModelState contains errors if any are encountered
            }

            //Check if this is a duplicate entry
            if (_npRepo.TrailExists(trailDto.Name))
            {
                ModelState.AddModelError("", "Trail Exists!");
                return StatusCode(404, ModelState);//We pass the ModelState as the value
            }

            //We need to convert Dto to Domain Model
            var trailObj = _mapper.Map<Trail>(trailDto);

            //If save was successfull
            if (!_npRepo.CreateTrail(trailObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {trailObj.Name}");
                return StatusCode(500, ModelState);
            }

            //Return object that was just created by calling API GetTrail
            //CreatedAtRoute will return a status of 201 for creation.
            return CreatedAtRoute("GetTrail", new { trailId = trailObj.Id }, trailObj);
        }

        [HttpPatch("{trailId:int}", Name = "UpdateTrail")]//We use this whenever we want to modify a record
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]//This enables us to show all responses in Documentation
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTrail(int trailId, [FromBody] TrailUpdateDto trailDto)
        {
            if (trailDto == null || trailId != trailDto.Id)
            {
                return BadRequest(ModelState);//ModelState contains errors if any are encountered
            }

            //We need to convert Dto to Domain Model
            var trailObj = _mapper.Map<Trail>(trailDto);

            //If save was not successfull
            if (!_npRepo.UpdateTrail(trailObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {trailObj.Name}");
                return StatusCode(500, ModelState);
            }
            //We dont return conten as with a Create
            return NoContent();
        }

        [HttpDelete("{trailId:int}", Name = "DeleteTrail")]//We use this whenever we want to delete a record
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteTrail(int trailId)
        {
            if (!_npRepo.TrailExists(trailId))
            {
                return NotFound();
            }

            //Get object to delete
            var trailObj = _npRepo.GetTrail(trailId);

            //Delete object
            if (!_npRepo.DeleteTrail(trailObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {trailObj.Name}");
                return StatusCode(500, ModelState);
            }
            //We dont return conten as with a Create
            return NoContent();
        }
    }
#pragma warning restore CS1591
}
