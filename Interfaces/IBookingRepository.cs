using secondProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace secondProject.Interfaces
{
    public interface IBookingRepository
    {
        Task<ICollection<Booking>> GetBookingsAsync();
        Task<Booking> GetBookingByIdAsync(int BookId);
        Task<bool> BookingExistsAsync(int BookId);
        Task<bool> DeleteBookingAsync(Booking booking);
        Task<bool> SaveAsync();
    }
}
