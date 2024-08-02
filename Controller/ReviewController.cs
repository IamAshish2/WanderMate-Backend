using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using secondProject.context;
using secondProject.Dtos;
using secondProject.Models;

namespace secondProject.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Reviews.ToList());
        }

        [HttpPost]
        public IActionResult Create([FromBody] ReviewDto reviewDto) { 

            var review = new Reviews{
                Rating = reviewDto.Rating,
                Comment = reviewDto.Comment,
                HotelId = reviewDto.HotelId
            };
                _context.Reviews.Add(review);
            _context.SaveChanges();
            return Ok("Added review Successfull");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {  
            var findHotel = _context.Reviews.Find(id);
            if (findHotel == null) { 
                   return NotFound();
            }
            _context.Reviews.Remove(findHotel);
            _context.SaveChanges();
            return Ok("Review Deleted Successfully");
        }

    }
}
