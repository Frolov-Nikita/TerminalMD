using TerminalMD;

var md = File.ReadAllText("Example.md");
var term = new Terminal();
term.WriteMd(md);
Console.ReadKey();
