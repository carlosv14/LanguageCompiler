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
        // if (this.Expressions.Any(x => x.GetType() != ExpressionType.String))
        // {
        //     throw new ApplicationException("Cannot implicitly convert all print parameters to string");
        // }
    }

    public override string GenerateCode() =>
        $"cout<<{string.Join("<<", this.Expressions.Select(x => x.GenerateCode()))}<<endl;";

    public override void Interpret()
    {
        foreach (var expr in Expressions)
        {
            var exprValue = expr.Evaluate();
            Console.Write(exprValue);
        }
    }
}