using LanguageCompiler.Core;

namespace ClassLibrary1;

public class ArithmeticExpression : BinaryExpression
{
    private readonly Dictionary<(ExpressionType, ExpressionType, TokenType), ExpressionType> _typeRules;
    public Token Operation { get; }
    
    public ArithmeticExpression(Expression leftExpression, Expression rightExpression, Token operation)
        : base(leftExpression, rightExpression)
    {
        Operation = operation;
        _typeRules = new Dictionary<(ExpressionType, ExpressionType, TokenType), ExpressionType>
        {
            {(ExpressionType.Int, ExpressionType.Int, TokenType.Plus), ExpressionType.Int},
            {(ExpressionType.Int, ExpressionType.Int, TokenType.Minus), ExpressionType.Int},
            {(ExpressionType.Int, ExpressionType.Int, TokenType.Asterisk), ExpressionType.Int},
            {(ExpressionType.Int, ExpressionType.Int, TokenType.Division), ExpressionType.Int},
            
            {(ExpressionType.Float, ExpressionType.Float, TokenType.Plus), ExpressionType.Float},
            {(ExpressionType.Float, ExpressionType.Float, TokenType.Minus), ExpressionType.Float},
            {(ExpressionType.Float, ExpressionType.Float, TokenType.Asterisk), ExpressionType.Float},
            {(ExpressionType.Float, ExpressionType.Float, TokenType.Division), ExpressionType.Float},

            {(ExpressionType.Float, ExpressionType.Float, TokenType.Plus), ExpressionType.Float},
            {(ExpressionType.Int, ExpressionType.Float, TokenType.Plus), ExpressionType.Float},
            {(ExpressionType.Float, ExpressionType.Int, TokenType.Plus), ExpressionType.Float},
            
            {(ExpressionType.String, ExpressionType.String, TokenType.Plus), ExpressionType.String},
        };
    }

    public override ExpressionType GetType()
    {
        var leftType = this.LeftExpression.GetType();
        var rightType = this.RightExpression.GetType();
        if (_typeRules.TryGetValue((leftType, rightType, Operation.TokenType), out var resultType))
        {
            return resultType;
        }

        throw new ApplicationException($"Cannot apply operator '{Operation.Lexeme}' to operands of type {leftType} and {rightType}");
    }
}