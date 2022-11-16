namespace LanguageCompiler.Core
{
    public class Token
    {
        public TokenType TokenType { get; set; }

        public string Lexeme { get; set; }

        public int Line { get; set; }

        public int Column { get; set; }

        public override string ToString()
        {
            return $"Lexema: {Lexeme}, Tipo: {TokenType}, fila: {Line}, columna: {Column}";
        }
    }
}
