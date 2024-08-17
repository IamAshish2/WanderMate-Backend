using secondProject.Models;

namespace secondProject.Interfaces
{
    public interface IDestinationRepository
    {   
        ICollection<Destination> GetDestinations();
        Destination GetDestinationById(int destId);
        bool CreateDestination(Destination destination);
        bool DeleteDestination(Destination destination);
        bool UpdateDestination(Destination destination);
        bool Save();
        bool DestinationsExists(int destId);
        Destination searchByName(string name);

    }
}
