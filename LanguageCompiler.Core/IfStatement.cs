namespace ClassLibrary1;

public class IfStatement : Statement
{
    public Expression Expression { get; set; }
    public Statement TrueStatement { get; set; }
    public Statement? FalseStatement { get; set; }

    public IfStatement(Expression expression, Statement trueStatement, Statement? falseStatement)
    {
        Expression = expression;
        TrueStatement = trueStatement;
        FalseStatement = falseStatement;
        this.ValidateSemantic();
    }

    public override void ValidateSemantic()
    {
        var exprType = this.Expression.GetType();
        if (exprType != ExpressionType.Bool)
        {
            throw new ApplicationException($"Cannot implicitly convert '{exprType}' to bool");
        }
    }
}