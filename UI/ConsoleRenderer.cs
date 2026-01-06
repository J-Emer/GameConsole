using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameConsole.UI
{

    public enum Dock{Left, Right, Bottom, Top, Center};

    public class ConsoleRenderer
    {
        public bool IsActive{get;set;} = true;
        private readonly SpriteFont _font;
        private readonly Texture2D _pixel;
    
        public Rectangle Bounds{get; private set;}
        public int Padding = 6;
        public int LineSpacing = 2;
    
        private int width;
        private int height;
        private GraphicsDeviceManager _graphics;
        private RasterizerState rasterizerState;
        //private Rectangle previousScissor;
        internal Dock _dockDirection;


        public ConsoleRenderer(GraphicsDeviceManager graphics, SpriteFont font, int _width, int _height, Dock _dock)
        {
            _graphics = graphics;
            _font = font;
            width = _width;
            height = _height;
            _dockDirection = _dock;
            
            _pixel = new Texture2D(graphics.GraphicsDevice, 1, 1);
            _pixel.SetData(new[] { Color.White });

            //previousScissor = graphics.GraphicsDevice.ScissorRectangle;

            rasterizerState = new RasterizerState
                                                {
                                                    ScissorTestEnable = true
                                                };            


            HandleDock();
        }
    
        private void HandleDock()
        {
            int windowHeight = _graphics.PreferredBackBufferHeight;
            int windowWidth = _graphics.PreferredBackBufferWidth;

            int x = 0;
            int y = 0;
            int w = 0;
            int h = 0;

            if(_dockDirection == Dock.Left)
            {
                x = 0;
                y = 0;
                w = width;
                h = windowHeight;
            }

            if(_dockDirection == Dock.Right)
            {
                x = windowWidth - width;
                y = 0;
                w = width;
                h = windowHeight;
            }

            if(_dockDirection == Dock.Bottom)
            {
                x = 0;
                y = windowHeight - height;
                w = windowWidth;
                h = height;
            }

            if(_dockDirection == Dock.Top)
            {
                x = 0;
                y = 0;
                w = windowWidth;
                h = height;
            }            

            if(_dockDirection == Dock.Center)
            {
                x = 0;
                y = 0;
                w = windowWidth;
                h = windowHeight;
            }  

            Bounds = new Rectangle(x,y,w,h);
        }

        public void Draw(SpriteBatch sb, ConsoleState state)
        {
            if (!IsActive)
                return;

            var device = _graphics.GraphicsDevice;

            Rectangle previous = device.ScissorRectangle;

            int lineHeight = _font.LineSpacing + LineSpacing;

            sb.Begin(
                SpriteSortMode.Immediate,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                DepthStencilState.None,
                rasterizerState
            );

            sb.Draw(_pixel, Bounds, Color.Black * 0.85f);

            var scrollArea = new Rectangle(
                Bounds.X + Padding,
                Bounds.Y + Padding,
                Bounds.Width - Padding * 2,
                Bounds.Height - (lineHeight + Padding * 2) - Padding
            );

            scrollArea = Rectangle.Intersect(scrollArea, device.Viewport.Bounds);
            device.ScissorRectangle = scrollArea;

            DrawLines(sb, state, scrollArea, lineHeight);

            device.ScissorRectangle = previous;

            DrawInput(sb, state, lineHeight);

            sb.End();

        }


        private void DrawLines(SpriteBatch sb, ConsoleState state, Rectangle area, int lineHeight)
        {
            int maxLines = area.Height / lineHeight;
            var lines = state.Lines.TakeLast(maxLines).ToList();

            for (int i = 0; i < lines.Count; i++)
            {
                var pos = new Vector2(
                    area.X,
                    area.Y + i * lineHeight);

                sb.DrawString(_font, lines[i], pos, Color.White);
            }
        }


        private void DrawInput(SpriteBatch sb, ConsoleState state, int lineHeight)
        {
            string prompt = "> ";
            string text = prompt + state.Input;
        
            var pos = new Vector2(
                Bounds.X + Padding,
                Bounds.Bottom - lineHeight - Padding);
        
            sb.DrawString(_font, text, pos, Color.White);
        
            // caret
            var prefixWidth = _font.MeasureString(prompt + state.Input[..state.CaretIndex]).X;
            var caretRect = new Rectangle(
                (int)(pos.X + prefixWidth),
                (int)pos.Y,
                2,
                _font.LineSpacing);
        
            sb.Draw(_pixel, caretRect, Color.White);
        }
    
    }
}