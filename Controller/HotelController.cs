using Azure.Messaging;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using secondProject.context;
using secondProject.Dtos;
//using secondProject.Migrations;
using secondProject.Models;

namespace secondProject.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public HotelController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Get request to hotels 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> Get()
        {
            try
            {
                var hotel = await _context.Hotels.ToListAsync();
                return Ok(hotel);
            }
            catch (Exception e)
            {
                return StatusCode(404, $"404 error occured. No Hotels found: {e.Message}");
            }
        }

        //[HttpPost]
        //public async Task<ActionResult<IEnumerable<Hotel>>> Create([FromBody] HotelDto hotelDto)
        //{
        //    try
        //    {

        //        var Hotel = new Hotel
        //        {
        //            Name = hotelDto.Name,
        //            Description = hotelDto.Description,
        //            ImageUrl = hotelDto.ImageUrl,
        //            Address = hotelDto.Address,
        //            Price = hotelDto.Price
        //        };

        //        _context.Hotels.Add(Hotel);
        //        await _context.SaveChangesAsync();
        //        return Ok("Hotel Created Successfully");
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, $"Internal server error: {e.Message}");
        //    }
        //}

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Hotel>>> Create([FromBody] Hotel hotel)
        {
            try
            {
                if (hotel == null)
                {
                    return BadRequest("Hotel data is null");
                }
                await _context.Hotels.AddAsync(hotel);
                await _context.SaveChangesAsync();
                return Ok(hotel);
            }
            catch (Exception e) {
                return BadRequest(e.Message);
            }

           
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetById(int id)
        {
            try
            {
                var hotel = await _context.Hotels.FindAsync(id);
                if (hotel == null)
                {
                    return NotFound();
                }
                return Ok(hotel);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Hotel>>> Delete(int id)
        {
            try
            {
                var hotel = await _context.Hotels.FindAsync(id);
                if (hotel == null)
                {
                    return NotFound();
                }
                _context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();
                return Ok("Deleted successfully");
            }
            catch (Exception e)
            {
                return BadRequest($"Invalid argument: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Hotel>>> UpdateHotel(int id, Hotel updateHotel)
        {
            try
            {
                var findHotel = await _context.Hotels.FindAsync(id);
                if (findHotel == null)
                {
                    return NotFound();
                }
                findHotel.Name = updateHotel.Name;
                findHotel.Description = updateHotel.Description;
                findHotel.ImageUrl = updateHotel.ImageUrl;
                findHotel.Price = updateHotel.Price;
                findHotel.FreeCancellation = updateHotel.FreeCancellation;
                findHotel.ReserveNow = updateHotel.ReserveNow;
               

                await _context.SaveChangesAsync();
                return Ok("Hotel successfully updated.");

            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Hotel>>> SearchByName([FromQuery] string name)
        {
            try
            {
                var hotels = await _context.Hotels
               .Where(h => h.Name.Contains(name))
                .ToListAsync();

                if (!hotels.Any())
                {
                    return NotFound();
                }

                return Ok(hotels);
            } catch(Exception e){
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }

    }
}
