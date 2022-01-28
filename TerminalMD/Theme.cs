using System;
using System.Collections.Generic;

namespace TerminalMD
{
    public class Theme : Dictionary<string, Style>
    {
        public string MarkerListItemLevel1 { get; set; } = "■";
        public string MarkerListItemLevel2 { get; set; } = "►";
        public string MarkerQuote { get; set; } = "|";

        public static Theme Default { get; } = new Theme
        {
            {"h1", new Style{
                BackColor = ConsoleColor.Gray,
                ForeColor = ConsoleColor.White,
                ParagraphOffset = 1,
            }},
            {"h2", new Style{
                BackColor = ConsoleColor.Gray,
                ForeColor = ConsoleColor.Black,
                ParagraphOffset = 2,
            }},
            {"h3", new Style{
                BackColor = ConsoleColor.Gray,
                ForeColor = ConsoleColor.Black,
                ParagraphOffset = 3,
            }},
            {"h4", new Style{
                BackColor = ConsoleColor.Black,
                ForeColor = ConsoleColor.White,
                ParagraphOffset = 4,
            }},
            {"p", new Style{
                BackColor = ConsoleColor.Black,
                ForeColor = ConsoleColor.Gray,
                ParagraphOffset = 3,
            }},
            {"b", new Style{
                BackColor = ConsoleColor.DarkGray,
                ForeColor = ConsoleColor.White,
                ParagraphOffset = 0,
            }},
            {"i", new Style{
                BackColor = ConsoleColor.DarkGray,
                ForeColor = ConsoleColor.Gray,
                ParagraphOffset = 0,
            }},
        };
        
        private static readonly Style DefaultStyle = new Style(){
            BackColor = ConsoleColor.Yellow,
            ForeColor = ConsoleColor.DarkRed,
            ParagraphOffset = 0,
        };

        public Style GetOrDefault(string key)
        {
            if(TryGetValue(key, out var style))
                return style;
            return DefaultStyle;
        }
    }
}
