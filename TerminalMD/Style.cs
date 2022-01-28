using System;
using System.Text;

namespace TerminalMD
{
    public class Style
    {
        public ConsoleColor BackColor { get; set; } = ConsoleColor.Black;
        public ConsoleColor ForeColor { get; set; } = ConsoleColor.Gray;
        public int ParagraphOffset { get; set; } = 0;
    }
}
