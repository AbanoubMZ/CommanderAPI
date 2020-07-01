using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public void CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var Commands = new List<Command>
            {
                new Command{Id=0,HowTo="Boil an egg",Line="Boil Water",Platform="Kitchen"},
                new Command{Id=1,HowTo="Eat an egg",Line="Bring a Dish",Platform="Livingroom"},
                new Command{Id=2,HowTo="Sleep",Line="Wash Teeth",Platform="Bedroom"}
            };
            return Commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command{Id=0,HowTo="Boil an egg",Line="Boil Water",Platform="Kitchen"};
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}