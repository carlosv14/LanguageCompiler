using LanguageCompiler.Core;

namespace ClassLibrary1;

public class ArrayAccessExpression : Expression
{
    public ExpressionType Type { get; }
    public IdExpression Id { get; }

    public Expression Index { get; }

    public ArrayAccessExpression(ExpressionType type, Token token, IdExpression id, Expression index)
    {
        Type = type;
        Id = id;
        Index = index;
    }
    
    public override ExpressionType GetType()
    {
        return Type;
    }
}