using System;
using GameConsole.GConsole;

namespace GameConsole.Commands
{
    public class Spawn : ICommand
    {
        public string Name => "spawn";

        public string Description => "Spawns a Gameobject";

        public string Usage => "spawn <X Position> <Y Position>";


        public void Load(CommandContext Context)
        {
            
        }
        public void Execute(CommandContext Context)
        {
            string objName = Context.Arguments[0];
            float x = float.Parse(Context.Arguments[1]);
            float y = float.Parse(Context.Arguments[2]);

            Context.WriteLine($"Spawning a {objName} at: x:{x}, y:{y}");
        }


    }
}