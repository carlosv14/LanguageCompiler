using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageCompiler.Lexer
{
    public readonly struct Position
    {
        public int Absolute { get; }

        public int Line { get; }

        public int Column { get; }

        public Position(int absolute, int line, int column )
        {
            Absolute = absolute;
            Line = line;
            Column = column;
        }

        public static Position Start => new Position(0, 0, 0);

        public Position MovePointer(char @char)
        {
            return @char == '\n'
                ? new Position(Absolute + 1, Line + 1, 1)
                : new Position(Absolute + 1, Line, Column + 1);
        }
    }
}
