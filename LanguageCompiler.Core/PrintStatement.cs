namespace ClassLibrary1;

public class PrintStatement :  Statement
{
    public List<Expression> Expressions { get; set; }

    public PrintStatement(List<Expression> expressions)
    {
        Expressions = expressions;
        this.ValidateSemantic();
    }

    public override void ValidateSemantic()
    {
        if (this.Expressions.Any(x => x.GetType() != ExpressionType.String))
        {
            throw new ApplicationException("Cannot implicitly convert all print parameters to string");
        }
    }
}