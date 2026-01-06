using System;

namespace GameConsole.GConsole
{
    public class CommandProcessor
    {
        private readonly CommandRegistry _registry;
        private readonly CommandParser _parser = new();
    
        private readonly Action<string> _writeLine;
    
        public CommandProcessor(CommandRegistry registry, Action<string> writeLine)
        {
            _registry = registry;
            _writeLine = writeLine;
        }
    
        public void Process(string input)
        {
            var parsed = _parser.Parse(input);
            if (parsed == null)
                return;
    
            if (!_registry.TryGet(parsed.Name, out var command))
            {
                _writeLine($"Unknown command: '{parsed.Name}'");
                return;
            }
    
            var context = new CommandContext(
                parsed.RawInput,
                parsed.Name,
                parsed.Arguments,
                _writeLine);
    
            try
            {
                command.Load(context);
                command.Execute(context);
            }
            catch (Exception ex)
            {
                _writeLine($"Error: {ex.Message}");
            }
        }
    }

}