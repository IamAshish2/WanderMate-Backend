using secondProject.context;
using secondProject.Interfaces;
using secondProject.Models;

namespace secondProject.Repository
{
    public class ThingsToDoRepository : IThingsToDoRepository
    {
        private readonly ApplicationDbContext _context;

        public ThingsToDoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool CreateThingsToDo(ThingsToDo thingsToDo)
        {
            _context.Add(thingsToDo);
            return Save();
        }

        public bool DeleteThingsToDo(ThingsToDo thingsToDo)
        {
             _context.Remove(thingsToDo);
            return Save();
        }

        public ThingsToDo GetThingsToDo(int id)
        {
            return _context.ThingsToDos.Where(t => t.Id == id).FirstOrDefault();
        }

        public ICollection<ThingsToDo> GetThingsToDoList()
        {
            return _context.ThingsToDos.OrderBy(t => t.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public ThingsToDo searchByName(string name)
        {
            var thingsToDo = _context.ThingsToDos.FirstOrDefault(t => t.Name == name);
            return thingsToDo;
        }

        public bool ThingsToDoExists(int thingsToDoId)
        {
            return _context.ThingsToDos.Any(t => t.Id == thingsToDoId);
        }

        public bool UpdateThingsToDo(ThingsToDo thingsToDo)
        {
            _context.Update(thingsToDo);
            return Save();
        }
    }
}
