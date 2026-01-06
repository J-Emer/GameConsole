using System.Collections.Generic;

namespace GameConsole.UI
{
    public class ConsoleState
    {
        public List<string> Lines = new();
        public int MaxLines = 200;
    
        public string Input = string.Empty;
        public int CaretIndex = 0;
    
        public List<string> History = new();
        public int HistoryIndex = -1;
    
        public void AddLine(string text)
        {
            Lines.Add(text);
            if (Lines.Count > MaxLines)
                Lines.RemoveAt(0);
        }
    }
    
}