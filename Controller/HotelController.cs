using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using secondProject.context;
using secondProject.Dtos;
using secondProject.Migrations;
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
        public IActionResult Get()
        {
            return Ok(_context.Hotels.ToList());
        }

        [HttpPost]
        public IActionResult Create([FromBody] HotelDto hotelDto)
        {
            var Hotel = new Hotel{
                Name = hotelDto.Name,
                Description = hotelDto.Description,
                ImageUrl = hotelDto.ImageUrl,
                Address = hotelDto.Address,
                Price = hotelDto.Price                
            };

            _context.Hotels.Add(Hotel);
            _context.SaveChanges();
            return Ok("Hotel Created Successfully");
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var hotel = _context.Hotels.Find(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return Ok(hotel);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var hotel = _context.Hotels.Find(id);
            if (hotel == null)
            {
                return NotFound();
            }
            _context.Hotels.Remove(hotel);
            _context.SaveChanges();
            return Ok("Deleted successfully");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateHotel(int id, Hotel updateHotel)
        {
            var findHotel = _context.Hotels.Find(id);
            if(findHotel == null)
            {
                return NotFound();
            }
            findHotel.Name = updateHotel.Name;
            findHotel.Description = updateHotel.Description;
            findHotel.ImageUrl = updateHotel.ImageUrl;
            findHotel.Price = updateHotel.Price;
            findHotel.Address = updateHotel.Address;

            _context.SaveChanges();
            return Ok("Hotel successfully updated.");
        }

        [HttpGet("search")]
        public IActionResult SearchByName([FromQuery] string name)
        {
            var hotels = _context.Hotels
                .Where(h => h.Name.Contains(name))
                .ToList();

            if (!hotels.Any())
            {
                return NotFound();
            }

            return Ok(hotels);
        }
    }
}
