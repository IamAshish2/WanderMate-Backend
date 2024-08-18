using secondProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace secondProject.Interfaces
{
    public interface IDestinationRepository
    {
        Task<ICollection<Destination>> GetDestinationsAsync();
        Task<Destination> GetDestinationByIdAsync(int destId);
        Task<bool> CreateDestinationAsync(Destination destination);
        Task<bool> DeleteDestinationAsync(Destination destination);
        Task<bool> UpdateDestinationAsync(Destination destination);
        Task<bool> SaveAsync();
        Task<bool> DestinationsExistsAsync(int destId);
        Task<Destination> SearchByNameAsync(string name);
    }
}
