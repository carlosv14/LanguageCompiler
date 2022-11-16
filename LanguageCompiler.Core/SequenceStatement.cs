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
}