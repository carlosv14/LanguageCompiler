using LanguageCompiler.Lexer;
using LanguageCompiler.Parser;

var code = File.ReadAllText("Code.txt").Replace(Environment.NewLine, "\n");
var input = new Input(code);
var scanner = new Scanner(input);

var parser = new Parser(scanner);

parser.Parse();
