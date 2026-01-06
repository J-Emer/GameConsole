using GameConsole.Commands;
using GameConsole.GConsole;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameConsole.UI;
using Microsoft.Xna.Framework.Input;


namespace GameConsole
{
    public class ConsoleWrapper
    {
        private ConsoleState console = new();
        private ConsoleInput input = new();
        private ConsoleRenderer renderer;
        private CommandProcessor processor;
        private CommandRegistry registry;
        public bool IsActive
        {
            get
            {
                return renderer.IsActive;
            }
            set
            {
                renderer.IsActive = value;
                if(value == true)
                {
                    HasFocus = true;
                }
            }
        }
        public bool HasFocus{get;set;} = true;

        private GraphicsDeviceManager _graphics;

        private KeyboardState _currentKeyboard;
        private KeyboardState _previousKeyboard;

        public Keys ToggleKey = Keys.F2;




        public ConsoleWrapper(GraphicsDeviceManager graphics, SpriteFont font, int width, int height, Dock _dock)
        {
            _graphics = graphics;

            renderer = new ConsoleRenderer(graphics, font, width, height, _dock);

            registry = new CommandRegistry();

            processor = new CommandProcessor(registry, text => console.AddLine(text));
            RegisterCommand(new HelpCommand(registry));
            RegisterCommand(new ClearCommand(console));
            RegisterCommand(new ClsCommand(console));

            console.AddLine("GameConsole  v0.0.1");

        }
        public void RegisterCommand(ICommand command)
        {
            registry.Register(command);
        }
        public void Update()
        {
            _previousKeyboard = _currentKeyboard;
            _currentKeyboard = Keyboard.GetState();

            if (_currentKeyboard.IsKeyDown(ToggleKey) && _previousKeyboard.IsKeyUp(ToggleKey))
            {
                IsActive = !IsActive;
            }

            if(!HasFocus){return;}
            if(!IsActive){return;}

            input.Update(console, command =>
            {
                console.AddLine("> " + command);
                processor.Process(command);
            });
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            renderer.Draw(spriteBatch, console);    
        }

    }
}