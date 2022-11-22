using LanguageCompiler.Core;

namespace ClassLibrary1;

public class ArrayType : ExpressionType
{
    public ExpressionType Of { get; }
    private readonly int _size;

    public ArrayType(string lexeme, TokenType tokenType, ExpressionType of, int size) : base(lexeme, tokenType)
    {
        Of = of;
        _size = size;
    }
}