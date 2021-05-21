using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
                new Command{Id=0, HowTo="Boli an egg", Line="Boil water", Platform="Kettle & Pan"},
                new Command{Id=0, HowTo="Cut bread", Line="Get a knife", Platform="knifee & chopping board"},
                new Command{Id=0, HowTo="Make cup of tea", Line="Place teabag in cup", Platform="Kettle & cup"}
            };
            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command{Id=0, HowTo="Boli an egg", Line="Boil water", Platform="Kettle & Pan"};
        }
    }
}