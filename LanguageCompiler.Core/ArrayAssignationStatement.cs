namespace ClassLibrary1;

public class ArrayAssignationStatement : Statement
{
    public IdExpression Id { get; }

    public Expression Index { get; }

    public Expression Expression { get; }

    private readonly Expression _access;

    public ArrayAssignationStatement(ArrayAccessExpression access, Expression expression)
    {
        Id = access.Id;
        Index = access.Index;
        Expression = expression;
        _access = access;
        this.ValidateSemantic();
    }
    public override void ValidateSemantic()
    {
        if (_access.GetType() is Array || Expression.GetType() is Array)
        {
            throw new ApplicationException($"Type {Expression.GetType()} is not assignable to {Id.GetType()}");
        }

        if (_access.GetType() != Expression.GetType())
        {
            throw new ApplicationException($"Type {Expression.GetType()} is not assignable to {Id.GetType()}");
        }
    }

    public override string GenerateCode() =>
        $"{this.Id.GenerateCode()}[{this.Index.GenerateCode()}] = {this.Expression.GenerateCode()};";

    public override void Interpret()
    {
        var symbol = this.Id.Evaluate();
        var index = this.Index.Evaluate();
        symbol[(int) index] = this.Expression.Evaluate();
        ContextManager.UpdateSymbol(this.Id.Name, symbol);
    }
}