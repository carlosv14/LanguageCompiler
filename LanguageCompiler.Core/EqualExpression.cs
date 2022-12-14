using LanguageCompiler.Core;

namespace ClassLibrary1;

public class EqualExpression : BinaryExpression
{
    private readonly Token _operation;
    private readonly Dictionary<(ExpressionType, ExpressionType), ExpressionType> _typeRules;
    public EqualExpression(Expression leftExpression, Expression rightExpression, Token operation)
        : base(leftExpression, rightExpression)
    {
        _operation = operation;
        _typeRules = new Dictionary<(ExpressionType, ExpressionType), ExpressionType>
        {
            {(ExpressionType.Int, ExpressionType.Int), ExpressionType.Bool},
            {(ExpressionType.Float, ExpressionType.Float), ExpressionType.Bool},
            {(ExpressionType.Int, ExpressionType.Float), ExpressionType.Bool},
            {(ExpressionType.Float, ExpressionType.Int), ExpressionType.Bool},
            {(ExpressionType.String, ExpressionType.String), ExpressionType.Bool},
            {(ExpressionType.Bool, ExpressionType.Bool), ExpressionType.Bool},
        };
    }

    public override ExpressionType GetType()
    {
        var leftType = this.LeftExpression.GetType();
        var rightType = this.RightExpression.GetType();
        if (_typeRules.TryGetValue((leftType, rightType), out var resultType))
        {
            return resultType;
        }

        throw new ApplicationException($"Cannot apply operator '=' to operands of type {leftType} and {rightType}");

    }

    public override string GenerateCode() =>
        $"{this.LeftExpression.GenerateCode()} {_operation.Lexeme} {this.RightExpression.GenerateCode()}";

    public override dynamic Evaluate()
    {
        if (_operation.TokenType == TokenType.Equal)
        {
            return LeftExpression.Evaluate() == RightExpression.Evaluate();
        }

        return LeftExpression.Evaluate() != RightExpression.Evaluate();
    }
}