namespace ClassLibrary1;

public class SequenceStatement :  Statement
{
    public Statement Current { get; set; }
    public Statement Next { get; set; }

    public SequenceStatement(Statement current, Statement next)
    {
        Current = current;
        Next = next;
        this.ValidateSemantic();
    }

    public override void ValidateSemantic()
    {
        this.Current?.ValidateSemantic();
        this.Next?.ValidateSemantic();
    }

    public override string GenerateCode() =>
        $"{this.Current?.GenerateCode()} {Environment.NewLine} {this.Next?.GenerateCode()}";

    public override void Interpret()
    {
        this.Current?.Interpret();
        this.Next?.Interpret();
    }
}