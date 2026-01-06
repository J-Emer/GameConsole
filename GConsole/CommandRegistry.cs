using System;
using System.Collections.Generic;

namespace GameConsole.GConsole
{
    public class CommandRegistry
    {
        private readonly Dictionary<string, ICommand> _commands = new(StringComparer.OrdinalIgnoreCase);
    
        public void Register(ICommand command)
        {
            if (_commands.ContainsKey(command.Name))
            {
                throw new InvalidOperationException($"Command '{command.Name}' is already registered.");                
            }
    
            _commands.Add(command.Name, command);
        }
    
        public bool TryGet(string name, out ICommand command)
        {
            return _commands.TryGetValue(name, out command);
        }
    
        public IEnumerable<ICommand> AllCommands => _commands.Values;
    }

}