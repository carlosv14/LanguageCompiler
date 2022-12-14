namespace ClassLibrary1;

public class IdExpression : Expression
{
    public string Name { get; set; }
    public ExpressionType Type { get; set; }

    public IdExpression(string name, ExpressionType type)
    {
        Name = name;
        Type = type;
    }

    public override ExpressionType GetType()
    {
        return Type;
    }

    public override string GenerateCode() =>
        this.Name;

    public override dynamic Evaluate() =>
        ContextManager.GetSymbolForInterpretation(this.Name).Value;
}