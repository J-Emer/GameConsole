namespace GameConsole.GConsole
{
    public interface ICommand
    {
        string Name{get;}
        string Description{get;}
        string Usage{get;}

        void Load(CommandContext Context);

        void Execute(CommandContext Context);
    }
}