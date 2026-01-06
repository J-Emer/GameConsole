using System;
using System.Linq;

namespace GameConsole.GConsole
{
    public class CommandParser
    {
        public ParsedCommand Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;
    
            var tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    
            return new ParsedCommand
            {
                RawInput = input,
                Name = tokens[0],
                Arguments = tokens.Skip(1).ToArray()
            };
        }
    }

    public class ParsedCommand
    {
        public string RawInput;
        public string Name;
        public string[] Arguments;
    }

}