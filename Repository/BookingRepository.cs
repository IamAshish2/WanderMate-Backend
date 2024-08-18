using Microsoft.EntityFrameworkCore;
using secondProject.context;
using secondProject.Interfaces;
using secondProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace secondProject.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> BookingExistsAsync(int BookId)
        {
            return await _context.Bookings.AnyAsync(b => b.BookingId == BookId);
        }

        public async Task<bool> DeleteBookingAsync(Booking booking)
        {
            _context.Remove(booking);
            return await SaveAsync();
        }

        public async Task<Booking> GetBookingByIdAsync(int BookId)
        {
            return await _context.Bookings.Where(b => b.BookingId == BookId).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Booking>> GetBookingsAsync()
        {
            return await _context.Bookings.OrderBy(b => b.BookingId).ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
