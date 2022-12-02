using LanguageCompiler.Core;

namespace ClassLibrary1;

public class RelationalExpression : BinaryExpression
{
    public Token Operation { get; }

    private readonly Dictionary<(ExpressionType, ExpressionType), ExpressionType> _typeRules;

    public RelationalExpression(Expression leftExpression, Expression rightExpression, Token operation)
        : base(leftExpression, rightExpression)
    {
        Operation = operation;
        _typeRules = new Dictionary<(ExpressionType, ExpressionType), ExpressionType>
        {
            { (ExpressionType.Int, ExpressionType.Int), ExpressionType.Bool},
            { (ExpressionType.Float, ExpressionType.Float), ExpressionType.Bool},
            { (ExpressionType.Int, ExpressionType.Float), ExpressionType.Bool},
            { (ExpressionType.Float, ExpressionType.Int), ExpressionType.Bool},

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

        throw new ApplicationException($"Cannot apply operator '{Operation.Lexeme}' to operands of type {leftType} and {rightType}");
    }

    public override string GenerateCode() =>
        $"{this.LeftExpression.GenerateCode()} {this.Operation.Lexeme} {this.RightExpression.GenerateCode()}";

    public override dynamic Evaluate()
    {
        switch (Operation.TokenType)
        {
            case TokenType.GreaterThan:
                return this.LeftExpression.Evaluate() > this.RightExpression.Evaluate();
            case TokenType.LessThan:
                return this.LeftExpression.Evaluate() < this.RightExpression.Evaluate();
            case TokenType.GreaterOrEqualThan:
                return this.LeftExpression.Evaluate() >= this.RightExpression.Evaluate();
            case TokenType.LessOrEqualThan:
                return this.LeftExpression.Evaluate() <= this.RightExpression.Evaluate();
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}