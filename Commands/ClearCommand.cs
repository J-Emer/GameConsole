using System;
using GameConsole.GConsole;
using GameConsole.UI;

namespace GameConsole.Commands
{
    public class ClearCommand : ICommand
    {
        public string Name => "clear";
        public string Description => "clears the console of all previouse commands & messages";
        public string Usage => "clear";
        private ConsoleState state;


        public ClearCommand(ConsoleState _state)
        {
            state = _state;    
        }
        public void Load(CommandContext Context)
        {
            
        }
        public void Execute(CommandContext Context)
        {
            state.Lines.Clear();
        }


    }
}