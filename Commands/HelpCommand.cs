using System.Linq;
using GameConsole.GConsole;

public class HelpCommand : ICommand
{
    private readonly CommandRegistry _registry;

    public HelpCommand(CommandRegistry registry)
    {
        _registry = registry;
    }

    public string Name => "help";
    public string Description => "Lists available commands.";
    public string Usage => "help <command>";



    public void Load(CommandContext Context)
    {
        
    }
    public void Execute(CommandContext context)
    {
        if (context.Arguments.Length == 0)
        {
            foreach (var cmd in _registry.AllCommands.OrderBy(c => c.Name))
            {
                context.WriteLine($"Name: {cmd.Name,-10} - Useage: {cmd.Usage}");
                context.WriteLine($"Description: {cmd.Description}");
                context.WriteLine(" ");
            }
            return;
        }

        var name = context.Arguments[0];
        if (_registry.TryGet(name, out var command))
        {
            context.WriteLine(command.Usage);
            context.WriteLine(command.Description);
        }
        else
        {
            context.WriteLine($"Unknown command '{name}'");
        }
    }


}
