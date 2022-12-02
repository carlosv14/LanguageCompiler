using LanguageCompiler.Lexer;
using LanguageCompiler.Parser;

var code = File.ReadAllText("Code.txt").Replace(Environment.NewLine, "\n");
var input = new Input(code);
var scanner = new Scanner(input);

var parser = new Parser(scanner);

var ast = parser.Parse();
ast.ValidateSemantic();
ast.Interpret();
//File.WriteAllText("./genCode.txt", generatedCode);
