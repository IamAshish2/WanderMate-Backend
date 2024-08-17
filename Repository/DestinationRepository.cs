using secondProject.context;
using secondProject.Interfaces;
using secondProject.Models;

namespace secondProject.Repository
{
    public class DestinationRepository : IDestinationRepository
    {
        private readonly ApplicationDbContext _context;

        public DestinationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool CreateDestination(Destination destination)
        {
            _context.Add(destination);
            return Save();

        }

        public bool DeleteDestination(int destId)
        {
            _context.Remove(destId);
            return Save();
        }

        public bool DeleteDestination(Destination destination)
        {
            _context.Remove(destination);
            return Save();
        }

        public bool DestinationsExists(int destId)
        {
            return _context.Destinations.Any(d => d.Id == destId);
        }

        public Destination GetDestinationById(int destId)
        {
            return _context.Destinations.Where(d => d.Id == destId).FirstOrDefault();
        }

        public ICollection<Destination> GetDestinations()
        {
            return _context.Destinations.OrderBy(d => d.Id).ToList();
        }

        public bool Save()
        {
           var saved =  _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public Destination searchByName(string name)
        {
            return _context.Destinations.FirstOrDefault(d => d.Name == name);
        }

        public bool UpdateDestination(Destination destination)
        {
            _context.Update(destination);
            return Save();
        }
    }
}
