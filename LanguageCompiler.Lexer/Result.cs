using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageCompiler.Lexer
{
    public static class Result
    {
        public static Result<T> Empty<T>(Input reminder) => new Result<T>(reminder);
        public static Result<T> Value<T>(T value, Input reminder) => new Result<T>(value, reminder);
    }
}
