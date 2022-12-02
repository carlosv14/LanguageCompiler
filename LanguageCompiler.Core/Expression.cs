namespace ClassLibrary1;

public abstract class Expression
{
    public abstract ExpressionType GetType();

    public abstract string GenerateCode();

    public abstract dynamic Evaluate();
}