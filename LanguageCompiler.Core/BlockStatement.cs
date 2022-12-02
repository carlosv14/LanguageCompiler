using System.Text;

namespace ClassLibrary1;

public class BlockStatement : Statement
{
    public Statement Statement { get; set; }

    public BlockStatement(Statement statement)
    {
        Statement = statement;
    }
    public override void ValidateSemantic()
    {
        this.Statement?.ValidateSemantic();
    }

    public override string GenerateCode()
    {
        var code = new StringBuilder();
        foreach (var symbol in ContextManager.GetSymbolsForCurrentContext())
        {
            var symbolType = symbol.Id.GetType();
            if (symbolType is ArrayType array)
            {
                symbolType = array.Of;
                code.Append($"vector<{symbolType.Lexeme}> {symbol.Id.Name}({array.Size}); {Environment.NewLine}");
            }
            else
            {
                code.Append($"{symbolType.Lexeme} {symbol.Id.Name}; {Environment.NewLine}");
            }
        }

        code.Append(this.Statement?.GenerateCode());
        return code.ToString();
    }

    public override void Interpret()
    {
        this.Statement?.Interpret();
    }
}