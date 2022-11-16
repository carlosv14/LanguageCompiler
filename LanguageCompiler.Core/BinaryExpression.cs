namespace ClassLibrary1;

public abstract class BinaryExpression : Expression
{
    public Expression LeftExpression { get; set; }
    public Expression RightExpression { get; set; }

    public BinaryExpression(Expression leftExpression, Expression rightExpression)
    {
        LeftExpression = leftExpression;
        RightExpression = rightExpression;
    }
}