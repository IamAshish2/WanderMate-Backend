using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using secondProject.Dtos;
using secondProject.Dtos.HotelDTOs;
using secondProject.Interfaces;
using secondProject.Models;

namespace secondProject.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class HotelController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelController(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        // GET request to hotels 
        [HttpGet]
        //[Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<IEnumerable<GetHotelDTO>>> Get()
        {
            try
            {
                var hotels = await _hotelRepository.GetHotelsAsync();
                var hotelDtos = hotels.Select(hotel => new GetHotelDTO
                {
                    Id = hotel.Id,
                    Name = hotel.Name,
                    Price = hotel.Price,
                    Description = hotel.Description,
                    ImageUrl = hotel.ImageUrl,
                    FreeCancellation = hotel.FreeCancellation,
                    ReserveNow = hotel.ReserveNow,
                }).ToList();

                return Ok(hotelDtos);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] HotelDto hotelDto)
        {
            try
            {
                var existingHotel = await _hotelRepository.SearchByNameAsync(hotelDto.Name);
                if (existingHotel != null) return BadRequest("The hotel already exists!");

                var hotel = new Hotel
                {
                    Name = hotelDto.Name,
                    Description = hotelDto.Description,
                    ImageUrl = hotelDto.ImageUrl,
                    Price = hotelDto.Price,
                    FreeCancellation = hotelDto.FreeCancellation,
                    ReserveNow = hotelDto.ReserveNow
                };

                var result = await _hotelRepository.CreateHotelAsync(hotel);
                if (!result) return BadRequest("The hotel could not be created. Try again later!");
                return Ok("Hotel Created Successfully");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDto>> GetById(int id)
        {
            try
            {
                var hotel = await _hotelRepository.GetHotelByIdAsync(id);
                if (hotel == null) return NotFound();

                var hotelDto = new HotelDto
                {
                    Name = hotel.Name,
                    Description = hotel.Description,
                    ImageUrl = hotel.ImageUrl,
                    Price = hotel.Price,
                    FreeCancellation = hotel.FreeCancellation,
                    ReserveNow = hotel.ReserveNow
                };

                return Ok(hotelDto);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var hotelExists = await _hotelRepository.HotelExistsAsync(id);
                if (!hotelExists) return NotFound();

                var result = await _hotelRepository.DeleteHotelAsync(id);
                if (!result) return BadRequest("Could not delete hotel!");
                return Ok("Deleted successfully");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateHotel(int id, [FromBody] HotelDto updateHotel)
        {
            try
            {
                var existingHotel = await _hotelRepository.GetHotelByIdAsync(id);
                if (existingHotel == null) return NotFound();

                existingHotel.Name = updateHotel.Name;
                existingHotel.Description = updateHotel.Description;
                existingHotel.ImageUrl = updateHotel.ImageUrl;
                existingHotel.Price = updateHotel.Price;
                existingHotel.FreeCancellation = updateHotel.FreeCancellation;
                existingHotel.ReserveNow = updateHotel.ReserveNow;

                var result = await _hotelRepository.UpdateHotelAsync(existingHotel);
                if (!result) return StatusCode(500, "Unsuccessful operation. Try again!");
                return Ok("Hotel successfully updated.");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<HotelDto>> SearchByName([FromQuery] string name)
        {
            try
            {
                var hotel = await _hotelRepository.SearchByNameAsync(name);
                if (hotel == null) return NotFound();

                var hotelDto = new GetHotelDTO
                {
                    Id = hotel.Id,
                    Name = hotel.Name,
                    Price = hotel.Price,
                    Description = hotel.Description,
                    ImageUrl = hotel.ImageUrl,
                    FreeCancellation = hotel.FreeCancellation,
                    ReserveNow = hotel.ReserveNow,
                };

                return Ok(hotelDto);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }
    }
}
