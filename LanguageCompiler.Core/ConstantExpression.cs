using LanguageCompiler.Core;

namespace ClassLibrary1;

public class ConstantExpression : Expression
{
    public ExpressionType Type { get; set; }

    public Token Token { get; set; }

    public ConstantExpression(ExpressionType type, Token token)
    {
        Type = type;
        Token = token;
    }

    public override ExpressionType GetType()
    {
        return Type;
    }
}