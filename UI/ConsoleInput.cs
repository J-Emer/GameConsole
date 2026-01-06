using System;
using Microsoft.Xna.Framework.Input;

namespace GameConsole.UI
{
    public class ConsoleInput
    {
        private KeyboardState _previous;

        public void Update(ConsoleState state, Action<string> onSubmit)
        {
            var keyboard = Keyboard.GetState();

            foreach (var key in keyboard.GetPressedKeys())
            {
                if (!_previous.IsKeyDown(key))
                {
                    HandleKey(key, state, onSubmit);
                }
            }

            _previous = keyboard;
        }

        private void HandleKey(Keys key, ConsoleState state, Action<string> onSubmit)
        {
            if (key == Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(state.Input))
                {
                    state.History.Add(state.Input);
                    state.HistoryIndex = state.History.Count;

                    onSubmit(state.Input);
                    state.Input = string.Empty;
                    state.CaretIndex = 0;
                }
                return;
            }

            if (key == Keys.Back && state.CaretIndex > 0)
            {
                state.Input = state.Input.Remove(state.CaretIndex - 1, 1);
                state.CaretIndex--;
                return;
            }

            if (key == Keys.Delete && state.CaretIndex < state.Input.Length)
            {
                state.Input = state.Input.Remove(state.CaretIndex, 1);
                return;
            }

            if (key == Keys.Left)
            {
                state.CaretIndex = Math.Max(0, state.CaretIndex - 1);
                return;
            }

            if (key == Keys.Right)
            {
                state.CaretIndex = Math.Min(state.Input.Length, state.CaretIndex + 1);
                return;
            }

            if (key == Keys.Up)
            {
                if (state.History.Count == 0) return;

                state.HistoryIndex = Math.Max(0, state.HistoryIndex - 1);
                state.Input = state.History[state.HistoryIndex];
                state.CaretIndex = state.Input.Length;
                return;
            }

            if (key == Keys.Down)
            {
                if (state.History.Count == 0) return;

                state.HistoryIndex = Math.Min(state.History.Count, state.HistoryIndex + 1);
                state.Input = state.HistoryIndex == state.History.Count
                    ? string.Empty
                    : state.History[state.HistoryIndex];
                state.CaretIndex = state.Input.Length;
                return;
            }

            char c = KeyToChar(key);
            if (c != '\0')
            {
                state.Input = state.Input.Insert(state.CaretIndex, c.ToString());
                state.CaretIndex++;
            }
        }

        private char KeyToChar(Keys key)
        {
            if (key >= Keys.A && key <= Keys.Z)
                return key.ToString().ToLower()[0];
        
            if (key >= Keys.D0 && key <= Keys.D9)
                return (char)('0' + (key - Keys.D0));
        
            if (key == Keys.Space)
                return ' ';
        
            return '\0';
        }




    }
}