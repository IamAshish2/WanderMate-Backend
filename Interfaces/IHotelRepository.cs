using System.Collections.Generic;
using System.Threading.Tasks;
using secondProject.Models;

namespace secondProject.Interfaces
{
    public interface IHotelRepository
    {
        Task<ICollection<Hotel>> GetHotelsAsync();
        Task<Hotel> GetHotelByIdAsync(int hotelId);
        Task<bool> CreateHotelAsync(Hotel hotel);
        Task<bool> DeleteHotelAsync(int hotelId);
        Task<bool> UpdateHotelAsync(Hotel hotel);
        Task<bool> HotelExistsAsync(int hotelId);
        Task<Hotel> SearchByNameAsync(string hotelName);
        Task<bool> SaveAsync();
    }
}
