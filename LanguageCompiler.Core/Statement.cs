namespace ClassLibrary1;

public abstract class Statement
{
    public abstract void ValidateSemantic();

    public abstract string GenerateCode();

    public abstract void Interpret();
}