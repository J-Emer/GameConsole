using System;
using GameConsole.GConsole;
using GameConsole.UI;

namespace GameConsole.Commands
{
    public class ClsCommand : ICommand
    {
        public string Name => "cls";
        public string Description => "clears the console of all previouse commands & messages";
        public string Usage => "cls";
        private ConsoleState state;


        public ClsCommand(ConsoleState _state)
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