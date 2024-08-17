using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using secondProject.Models;
using secondProject.Interfaces;
using AutoMapper;
using secondProject.Dtos.DestinationDtos;


namespace secondProject.Controller
{
    [ApiController]
    [Route("api/[Controller]")]
    public class DestinationsController : ControllerBase
    {
        private readonly IDestinationRepository _destinationRepository;
        private readonly IMapper _mapper;

        public DestinationsController(IDestinationRepository destinationRepository, IMapper mapper)
        {
            _destinationRepository = destinationRepository;
            _mapper = mapper;
        }

        // GET: Destinations
        [HttpGet]

        public IActionResult GetDestinations()
        {

            var destinations = _destinationRepository.GetDestinations();
            if (destinations == null) return NotFound();
            return Ok(destinations);
        }

        // GET: Destinations/Details/5
        [HttpGet("Destinations/{id}")]
        public IActionResult GetDestinationById(int id)
        {
            if (!_destinationRepository.DestinationsExists(id) || id == null) return NotFound();
            var destination = _destinationRepository.GetDestinationById(id);
            if (destination == null) return NotFound(); 
            if(!ModelState.IsValid) return BadRequest();
            return Ok(destination);
        }

        // GET: Destinations/Create
        [HttpPost]
        public IActionResult CreateDestination([FromBody] DestinationDto destinationDto)
        {
            var destinations = _destinationRepository.GetDestinations().Where(d => d.Name == destinationDto.Name).FirstOrDefault();
            if(destinations != null) return BadRequest("The destination already exists!");

            var destination = new Destination
            {
                Name = destinationDto.Name,
                Price = destinationDto.Price,
                ImageUrls = destinationDto.ImageUrls,
                Description = destinationDto.Description,
            };

            if(!_destinationRepository.CreateDestination(destination)) return BadRequest("The destination could not be created.Try again later!");
            return Ok(destination);
        }


        [HttpDelete("{destId}")]
        public IActionResult DeleteDestination(int destId)
        {
            if (!_destinationRepository.DestinationsExists(destId) || destId == null) return NotFound();

            var destination = _destinationRepository.GetDestinationById(destId);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!_destinationRepository.DeleteDestination(destination)) return BadRequest("Could not delete Destination!");
            return Ok("Deletion Successfull");
        }

        [HttpPut("{destId}")]
        public IActionResult UpdateThingsToDo(int destId, [FromBody] DestinationDto destination)
        {
            if (!_destinationRepository.DestinationsExists(destId)) return BadRequest("Things to do does not exist!");
            if (destination == null) return BadRequest(ModelState);

            var existingDestination = _destinationRepository.GetDestinationById(destId);

            existingDestination.Name = destination.Name;
            existingDestination.Price = destination.Price;
            existingDestination.ImageUrls = destination.ImageUrls;
            existingDestination.Description = destination.Description;

            if (!_destinationRepository.UpdateDestination(existingDestination))
            {
                ModelState.AddModelError("", "Unsuccessfull operation. Try again!");
                return StatusCode(500, ModelState);
            }
            return Ok("Succes");
        }


    }
}
