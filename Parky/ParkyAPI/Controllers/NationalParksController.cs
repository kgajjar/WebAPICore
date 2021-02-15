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
    [Route("api/v{version:apiVersion}/nationalparks")]
    //[Route("api/[controller]")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "ParkyOpenAPISpecNP")]//Here we add the API calls to dropdown of Swagger. (Bundling them)
    [ProducesResponseType(StatusCodes.Status400BadRequest)]//This is applicable to any method
    public class NationalParksController : ControllerBase////Since Controller class derives from ControllerBase. We use Controller base as this isnt using views like a MVC app does.
    {
        //Get repo using DI
        private INationalParkRepository _npRepo;
        private readonly IMapper _mapper;

        public NationalParksController(INationalParkRepository npRepo, IMapper mapper)
        {
            _npRepo = npRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of National Parks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<NationalParkDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetNationalParks()
        {
            var objList = _npRepo.GetNationalParks();

            //Obj to hold Dto model
            var objDto = new List<NationalParkDto>();

            foreach (var obj in objList)
            {
                /*
                 1) Convert output to - NationalParkDto
                 2) Source Object - obj
                 */
                objDto.Add(_mapper.Map<NationalParkDto>(obj));
            }
            //This returns Status Code: 200 meaning OK
            return Ok(objDto);
        }

        //To avoid ambiguos error you have to specify param in Http Get below to differentiate between 2 methods with same name
        //We also specify that param is integer
        /// <summary>
        /// Get individual National Parks
        /// </summary>
        /// <param name="nationalParkId">The Id of the National Park</param>
        /// <returns></returns>
        [HttpGet("{nationalParkId:int}", Name = "GetNationalPark")]
        [ProducesResponseType(200, Type = typeof(NationalParkDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]//This enables us to show all responses in Documentation
        [ProducesDefaultResponseType]//Any error that is not one of the above
        [Authorize]//Will only allow authorized users to access this API call
        public IActionResult GetNationalPark(int nationalParkId)
        {
            var obj = _npRepo.GetNationalPark(nationalParkId);
            if (obj == null)
            {
                return NotFound();
            }

            /*
                 1) Convert output to - NationalParkDto
                 2) Source Object - obj
            */
            var objDto = _mapper.Map<NationalParkDto>(obj);

            //This is how we would have to map the classes if we didn't have AutoMapper
            //var objDto = new NationalParkDto()
            //{
            //    Created = obj.Created,
            //    Id = obj.Id,
            //    Name = obj.Name,
            //    State = obj.State
            //}

            //This returns Status Code: 200 meaning OK
            return Ok(objDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(NationalParkDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]//This enables us to show all responses in Documentation
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateNationalPark([FromBody] NationalParkDto nationalParkDto)//We specify what we will get from the Body
        {
            if (nationalParkDto == null)
            {
                return BadRequest(ModelState);//ModelState contains errors if any are encountered
            }

            //Check if this is a duplicate entry
            if (_npRepo.NationalParkExists(nationalParkDto.Name))
            {
                ModelState.AddModelError("", "National Park Exists!");
                return StatusCode(404, ModelState);//We pass the ModelState as the value
            }

            //We need to convert Dto to Domain Model
            var nationalParkObj = _mapper.Map<NationalPark>(nationalParkDto);

            //If save was successfull
            if (!_npRepo.CreateNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {nationalParkObj.Name}");
                return StatusCode(500, ModelState);
            }

            //Return object that was just created by calling API GetNationalPark
            //CreatedAtRoute will return a status of 201 for creation.
            return CreatedAtRoute("GetNationalPark", new
            {
                version = HttpContext.GetRequestedApiVersion().ToString(),
                nationalParkId = nationalParkObj.Id
            }, nationalParkObj);
        }

        [HttpPatch("{nationalParkId:int}", Name = "UpdateNationalPark")]//We use this whenever we want to modify a record
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]//This enables us to show all responses in Documentation
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateNationalPark(int nationalParkId, [FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null || nationalParkId != nationalParkDto.Id)
            {
                return BadRequest(ModelState);//ModelState contains errors if any are encountered
            }

            //We need to convert Dto to Domain Model
            var nationalParkObj = _mapper.Map<NationalPark>(nationalParkDto);

            //If save was not successfull
            if (!_npRepo.UpdateNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {nationalParkObj.Name}");
                return StatusCode(500, ModelState);
            }
            //We dont return content as with a Create
            return NoContent();
        }

        [HttpDelete("{nationalParkId:int}", Name = "DeleteNationalPark")]//We use this whenever we want to delete a record
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteNationalPark(int nationalParkId)
        {
            if (!_npRepo.NationalParkExists(nationalParkId))
            {
                return NotFound();
            }

            //Get object to delete
            var nationalParkObj = _npRepo.GetNationalPark(nationalParkId);

            //Delete object
            if (!_npRepo.DeleteNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {nationalParkObj.Name}");
                return StatusCode(500, ModelState);
            }
            //We dont return content as with a Create
            return NoContent();
        }
    }
#pragma warning restore CS1591
}
