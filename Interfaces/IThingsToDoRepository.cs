using secondProject.Models;

namespace secondProject.Interfaces
{
    public interface IThingsToDoRepository
    {
        ICollection<ThingsToDo> GetThingsToDoList();
        ThingsToDo GetThingsToDo(int id);
        bool ThingsToDoExists(int thingsToDoId);
        bool Save();
        bool DeleteThingsToDo(ThingsToDo thingsToDo);
        bool CreateThingsToDo(ThingsToDo  thingsToDo);
        bool UpdateThingsToDo(ThingsToDo thingsToDo);
        ThingsToDo searchByName(string name);
    }
}
