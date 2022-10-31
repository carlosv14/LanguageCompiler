using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageCompiler.Lexer
{
    public class Result<T>
    {
        internal Result(T value, Input reminder)
        {
            Value = value;
            Reminder = reminder;
            HasValue = true;
        }

        internal Result(Input reminder)
        {
            Reminder = reminder;
            HasValue = false;
        }

        public T Value { get; set; }

        public Input Reminder { get; set; }

        public bool HasValue { get; set; }
    }
}
