namespace ClassLibrary1;

public class Symbol
{
    public IdExpression Id { get; set; }

    public dynamic Value { get; set; }

    public Symbol(IdExpression id)
    {
        Id = id;
    }
    
    public Symbol(IdExpression id, dynamic value)
    {
        Id = id;
        Value = value;
    }
}