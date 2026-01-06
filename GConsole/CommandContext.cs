using System;

namespace GameConsole.GConsole
{
    public class CommandContext
{
    public string RawInput { get; }
    public string CommandName { get; }
    public string[] Arguments { get; }

    public Action<string> WriteLine { get; }

    public CommandContext(
        string rawInput,
        string commandName,
        string[] arguments,
        Action<string> writeLine)
    {
        RawInput = rawInput;
        CommandName = commandName;
        Arguments = arguments;
        WriteLine = writeLine;
    }
}

}