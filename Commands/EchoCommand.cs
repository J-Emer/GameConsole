using GameConsole.GConsole;

namespace GameConsole.Commands
{
    public class EchoCommand : ICommand
    {
        public string Name => "echo";
        public string Description => "Prints text to the console.";
        public string Usage => "echo <text>";

        public void Load(CommandContext Context)
        {
            
        }
        public void Execute(CommandContext context)
        {
            if (context.Arguments.Length == 0)
            {
                context.WriteLine("Usage: " + Usage);
                return;
            }

            context.WriteLine(string.Join(" ", context.Arguments));
        }


    }

}