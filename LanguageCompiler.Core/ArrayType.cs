using LanguageCompiler.Core;

namespace ClassLibrary1;

public class ArrayType : ExpressionType
{
    public ExpressionType Of { get; }
    public int Size { get; }

    public ArrayType(string lexeme, TokenType tokenType, ExpressionType of, int size) : base(lexeme, tokenType)
    {
        Of = of;
        Size = size;
    }
}