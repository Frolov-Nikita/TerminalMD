using System;
using System.Collections.Generic;
using Markdig;
using Markdig.Syntax.Inlines;
using Markdig.Syntax;

namespace TerminalMD
{
    public class Terminal
    {
        private static readonly string[] headers = new string[] 
        {"h1", "h2", "h3", "h4"};

        private (ConsoleColor back, ConsoleColor front) original;

        public Theme Theme { get; }

        public Terminal(Theme theme)
        {
            Theme = theme;
        }

        public Terminal()
        {
            Theme = Theme.Default;
        }

        public void WriteMd(string mdText)
        {
            original = (Console.BackgroundColor, Console.ForegroundColor);

            var doc = Markdown.Parse(mdText);
            foreach (var item in doc)
                WriteBlock(item, mdText);

            (Console.BackgroundColor, Console.ForegroundColor) = original;
        }

        private void WriteBlock(Block block, string txtSource)
        {
            switch (block)
            {
                case BlankLineBlock blankLineBlock:
                    Console.WriteLine();
                    break;
                case CodeBlock codeBlock:

                    break;
                //case Markdig.Syntax.ContainerBlock containerBlock:

                //    break;
                case EmptyBlock emptyBlock:

                    break;
                //case Markdig.Syntax.FencedCodeBlock fencedCodeBlock:

                //    break;
                case HeadingBlock header:
                    WriteHeadingBlock(header);
                    break;
                case HtmlBlock htmlBlock:

                    break;
                case ListBlock listBlock:
                    WriteListBlock(listBlock);
                    break;
                case ParagraphBlock paragraphBlock:
                    WriteParagraphBlock(paragraphBlock);
                    break;
                case QuoteBlock quoteBlock:
                    WriteQuoteBlock(quoteBlock);
                    break;
                case ThematicBreakBlock thematicBreakBlock:

                    break;                
                default:
                    var style = Theme.GetOrDefault("undefined");
                    SetColors(style);
                    var txt = txtSource.Substring(block.Span.Start, block.Span.Length);
                    Console.Write(txt);
                    break;

            }
            Console.WriteLine();
        }

        private void WriteQuoteBlock(QuoteBlock quoteBlock)
        {
            
        }

        private void WriteParagraphBlock(ParagraphBlock paragraphBlock)
        {
            var style = Theme.GetOrDefault("p");
            WriteInlineContainer(paragraphBlock.Inline, style);
        }

        private void WriteHeadingBlock(HeadingBlock header)
        {
            var key = headers.Length > header.Level
                ? headers[header.Level]
                : headers[headers.Length - 1];
            var style = Theme.GetOrDefault(key);
            SetColors(style);
            var offset = new String(' ', style.ParagraphOffset);
            Console.Write(offset);
            WriteInlineContainer(header.Inline, style);
            Console.Write(offset);
        }

        private void WriteListBlock(ListBlock listBlock)
        {
            var i = 1;
            foreach (ListItemBlock item in listBlock)
                WriteListItemBlock(item, i++);
        }

        private void WriteListItemBlock(ListItemBlock listItemBlock, int order = 0, int perentOrder = 0)
        {
            var parent = (ListBlock)listItemBlock.Parent;
            var isOrdered = parent.IsOrdered;

            foreach (var item in listItemBlock)
            {
                if (item is ParagraphBlock paragraphBlock)
                {
                    string marker;
                    if (isOrdered)
                    {
                        if (perentOrder > 0)
                            marker = $"   {perentOrder}.{order}.";
                        else
                            marker = $"{order}.";
                    }
                    else
                    {
                        if (perentOrder > 0)
                            marker = $"   {Theme.MarkerListItemLevel2}";
                        else
                            marker = $"{Theme.MarkerListItemLevel1}";
                    }
                    Console.Write(marker + " ");
                    WriteInlineContainer(paragraphBlock.Inline);
                }                    

                if (item is ListBlock listBlock)
                {
                    var i = 1;
                    foreach (ListItemBlock subItem in listBlock)
                        WriteListItemBlock(subItem, i++, order);
                }
                Console.WriteLine();
            }
        }

        private static void SetColors(Style style)
        {
            Console.ForegroundColor = style.ForeColor;
            Console.BackgroundColor = style.BackColor;
        }

        private void WriteInlineContainer(ContainerInline inline, Style style = null)
        {
            foreach (var item in inline)
            {
                if(style != default)
                    SetColors(style);
                switch (item)
                {
                    case AutolinkInline autolinkInline:

                        break;
                    case CodeInline codeInline:

                        break;
                    case EmphasisDelimiterInline emphasisDelimiterInline:

                        break;
                    case EmphasisInline emphasisInline:
                        var innerStyle = Theme.GetOrDefault(emphasisInline.DelimiterCount == 2 ? "b" : "i");
                        SetColors(innerStyle);
                        WriteInlineContainer(emphasisInline, innerStyle);
                        break;
                    case HtmlEntityInline htmlEntityInline:

                        break;
                    case HtmlInline htmlInline:

                        break;
                    case LineBreakInline lineBreakInline:
                        if (lineBreakInline.IsHard)
                            Console.WriteLine();
                        break;
                    case LinkDelimiterInline linkDelimiterInline:

                        break;
                    case LinkInline linkInline:

                        break;
                    case LiteralInline literalInline:
                        WriteLiteralInline(literalInline);
                        break;
                    default:
                        break;
                }
            }
        }

        private void WriteLiteralInline(LiteralInline literalInline)
        {
            var text = literalInline.Content.ToString();
            Console.Write(text);
        }
    }
}
