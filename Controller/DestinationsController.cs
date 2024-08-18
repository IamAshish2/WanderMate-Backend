using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetDestinationsAsync()
        {
            var destinations = await _destinationRepository.GetDestinationsAsync();
            if (destinations == null) return NotFound();
            return Ok(destinations);
        }

        // GET: Destinations/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDestinationByIdAsync(int id)
        {
            if (id == 0 || !await _destinationRepository.DestinationsExistsAsync(id)) return NotFound();
            var destination = await _destinationRepository.GetDestinationByIdAsync(id);
            if (destination == null) return NotFound();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(destination);
        }

        // POST: Destinations/Create
        [HttpPost]
        public async Task<IActionResult> CreateDestinationAsync([FromBody] DestinationDto destinationDto)
        {
            var existingDestination = await _destinationRepository.SearchByNameAsync(destinationDto.Name);
            if (existingDestination != null) return BadRequest("The destination already exists!");

            var destination = new Destination
            {
                Name = destinationDto.Name,
                Price = destinationDto.Price,
                ImageUrl = destinationDto.ImageUrl,
                Description = destinationDto.Description,
            };

            if (!await _destinationRepository.CreateDestinationAsync(destination))
                return BadRequest("The destination could not be created. Try again later!");

            return Ok(destination);
        }

        // DELETE: Destinations/Delete/5
        [HttpDelete("{destId}")]
        public async Task<IActionResult> DeleteDestinationAsync(int destId)
        {
            if (destId == 0 || !await _destinationRepository.DestinationsExistsAsync(destId)) return NotFound();

            var destination = await _destinationRepository.GetDestinationByIdAsync(destId);
            if (destination == null) return NotFound();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!await _destinationRepository.DeleteDestinationAsync(destination))
                return BadRequest("Could not delete the destination!");

            return Ok("Deletion successful");
        }

        // PUT: Destinations/Update/5
        [HttpPut("{destId}")]
        public async Task<IActionResult> UpdateDestinationAsync(int destId, [FromBody] DestinationDto destinationDto)
        {
            if (destId == 0 || !await _destinationRepository.DestinationsExistsAsync(destId))
                return BadRequest("Destination does not exist!");

            if (destinationDto == null) return BadRequest(ModelState);

            var existingDestination = await _destinationRepository.GetDestinationByIdAsync(destId);
            if (existingDestination == null) return NotFound();

            existingDestination.Name = destinationDto.Name;
            existingDestination.Price = destinationDto.Price;
            existingDestination.ImageUrl = destinationDto.ImageUrl;
            existingDestination.Description = destinationDto.Description;

            if (!await _destinationRepository.UpdateDestinationAsync(existingDestination))
            {
                ModelState.AddModelError("", "Unsuccessful operation. Try again!");
                return StatusCode(500, ModelState);
            }

            return Ok("Success");
        }
    }
}
