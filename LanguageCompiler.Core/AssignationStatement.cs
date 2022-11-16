namespace ClassLibrary1;

public class AssignationStatement : Statement
{
    public IdExpression Id { get; set; }
    public Expression Expression { get; set; }

    public AssignationStatement(IdExpression id, Expression expression)
    {
        Id = id;
        Expression = expression;
        this.ValidateSemantic();
    }

    public override void ValidateSemantic()
    {
        var idType = this.Id.GetType();
        var exprType = this.Expression.GetType();
        if (idType != exprType)
        {
            throw new ApplicationException($"Cannot convert source type '{exprType}' to {idType}");
        }
    }
}