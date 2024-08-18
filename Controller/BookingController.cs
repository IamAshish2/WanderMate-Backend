using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using secondProject.Interfaces;
using secondProject.Models;

namespace secondProject.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        // Get all bookings
        [HttpGet]
        public async Task<IActionResult> GetBookings()
        {
            var bookings = await _bookingRepository.GetBookingsAsync();
            return Ok(bookings);
        }

        // Get booking by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }

        // Delete booking by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            if (!await _bookingRepository.BookingExistsAsync(id)) return NotFound();
            var booking = await _bookingRepository.GetBookingByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            bool isDeleted = await _bookingRepository.DeleteBookingAsync(booking);
            if (!isDeleted)
            {
                return StatusCode(500, "Error deleting the booking");
            }

            return NoContent();
        }
    }
}
