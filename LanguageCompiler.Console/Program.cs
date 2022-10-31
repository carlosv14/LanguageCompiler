using LanguageCompiler.Lexer;

var code = File.ReadAllText("Code.txt").Replace(Environment.NewLine, "\n");
var input = new Input(code);
var scanner = new Scanner(input);
var token = scanner.GetNextToken();

while (token.TokenType != TokenType.EOF )
{
    Console.WriteLine(token);
    token = scanner.GetNextToken();
}
