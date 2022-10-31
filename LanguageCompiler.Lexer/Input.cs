using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageCompiler.Lexer
{
    public readonly struct Input
    {
        public string Source { get; }

        public int Length { get; }

        public Position Position { get; }

        public Input(string source)
            : this(source, Position.Start, source.Length)
        {

        }

        public Input(string source, Position position, int length)
        {
            Source = source;
            Position = position;
            Length = length;
        }

        public Result<char> NextChar()
        {
            if (Length == 0)
            {
                return Result.Empty<char>(this);
            }

            var @char = Source[Position.Absolute];
            return Result.Value(@char, new Input(Source, Position.MovePointer(@char), Length - 1));
        }
    }
}
