namespace ClassLibrary1;

public class LogicalAndExpression : BinaryExpression
{
    private readonly Dictionary<(ExpressionType, ExpressionType), ExpressionType> _typeRules;
    public LogicalAndExpression(Expression leftExpression, Expression rightExpression)
        : base(leftExpression, rightExpression)
    {
        _typeRules = new Dictionary<(ExpressionType, ExpressionType), ExpressionType>
        {
            { (ExpressionType.Bool, ExpressionType.Bool), ExpressionType.Bool }
        };
    }

    public override ExpressionType GetType()
    {
        var leftType = this.LeftExpression.GetType();
        var rightType = this.RightExpression.GetType();
        if (_typeRules.TryGetValue((leftType, rightType), out var resultType ))
        {
            return resultType;
        }

        throw new ApplicationException($"Cannot apply operator '&&' to operands of type {leftType} and {rightType}");
    }
}