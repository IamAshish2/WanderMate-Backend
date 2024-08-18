using Microsoft.EntityFrameworkCore;
using secondProject.context;
using secondProject.Interfaces;
using secondProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace secondProject.Repository
{
    public class DestinationRepository : IDestinationRepository
    {
        private readonly ApplicationDbContext _context;

        public DestinationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateDestinationAsync(Destination destination)
        {
            await _context.AddAsync(destination);
            return await SaveAsync();
        }

        public async Task<bool> DeleteDestinationAsync(int destId)
        {
            var destination = await _context.Destinations.FindAsync(destId);
            if (destination != null)
            {
                _context.Remove(destination);
                return await SaveAsync();
            }
            return false;
        }

        public async Task<bool> DeleteDestinationAsync(Destination destination)
        {
            _context.Remove(destination);
            return await SaveAsync();
        }

        public async Task<bool> DestinationsExistsAsync(int destId)
        {
            return await _context.Destinations.AnyAsync(d => d.Id == destId);
        }

        public async Task<Destination> GetDestinationByIdAsync(int destId)
        {
            return await _context.Destinations.FirstOrDefaultAsync(d => d.Id == destId);
        }

        public async Task<ICollection<Destination>> GetDestinationsAsync()
        {
            return await _context.Destinations.OrderBy(d => d.Id).ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<Destination> SearchByNameAsync(string name)
        {
            return await _context.Destinations.FirstOrDefaultAsync(d => d.Name == name);
        }

        public async Task<bool> UpdateDestinationAsync(Destination destination)
        {
            _context.Update(destination);
            return await SaveAsync();
        }
    }
}
