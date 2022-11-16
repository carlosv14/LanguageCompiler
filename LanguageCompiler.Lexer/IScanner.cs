using LanguageCompiler.Core;

namespace LanguageCompiler.Lexer
{
    public interface IScanner
    {
        Token GetNextToken();
    }
}