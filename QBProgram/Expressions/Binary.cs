using System;
using System.Collections.Generic;

namespace QBasic.Program.Expressions
{
    public class Binary : Expression
    {
        private static readonly IReadOnlyDictionary<Operators, char> OperatorChars;

        static Binary()
        {
            OperatorChars = new Dictionary<Operators, char>()
            {
                { Operators.Asterisk, '*' },
                { Operators.Backslash, '\\' },
                { Operators.Minus, '-' },
                { Operators.Percent, '%' },
                { Operators.Plus, '+' },
                { Operators.Slash, '/' }
            };
        }

        public enum Operators
        {
            Plus,
            Minus,
            Asterisk,
            Slash,
            Backslash,
            Percent
        }

        public Expression Left { get; set; }

        public Operators Operator { get; set; }

        public Expression Right { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", Left.ToString(), OperatorChars[Operator], Right.ToString());
        }
    }
}
