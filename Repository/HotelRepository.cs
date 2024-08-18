using Microsoft.EntityFrameworkCore;
using secondProject.context;
using secondProject.Interfaces;
using secondProject.Models;

namespace secondProject.Repository
{
    public class HotelRepository : IHotelRepository
    {
        private readonly ApplicationDbContext _context;

        public HotelRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateHotelAsync(Hotel hotel)
        {
             await _context.AddAsync(hotel);
            return await SaveAsync();
        }

        public async Task<bool> DeleteHotelAsync(int hotelId)
        {
            var hotel = await _context.Hotels.FindAsync(hotelId);
            if (hotel == null) return false;
            _context.Hotels.Remove(hotel);
            return await SaveAsync();
        }

        public async Task<Hotel> GetHotelByIdAsync(int hotelId)
        {
            return await _context.Hotels.Where(h => h.Id == hotelId).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Hotel>> GetHotelsAsync()
        {
            return await _context.Hotels.OrderBy(h => h.Id).ToListAsync();
        }

        public async Task<bool> HotelExistsAsync(int hotelId)
        {
            return await _context.Hotels.AnyAsync(h => h.Id == hotelId);
        }

        public  async Task<bool> SaveAsync()
        {
            var saved =  await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<Hotel> SearchByNameAsync(string hotelName)
        {
            return await _context.Hotels.Where(h => h.Name == hotelName).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateHotelAsync(Hotel hotel)
        {
            _context.Update(hotel);
            return await SaveAsync();
        }
    }
}
