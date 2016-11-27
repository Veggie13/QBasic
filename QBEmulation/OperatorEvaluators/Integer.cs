using QBasic.Program.Expressions;
using QBasic.Types;
using System;
using System.Collections.Generic;

namespace QBasic.Emulation.OperatorEvaluators
{
    using OperatorSelector = IReadOnlyDictionary<Binary.Operators, DataTypeOperatorEvaluator>;

    abstract class Integer : Primitive<Int16>
    {
        public static readonly OperatorSelector OperatorSelector;

        static Integer()
        {
            OperatorSelector = new Dictionary<Binary.Operators, DataTypeOperatorEvaluator>()
            {
                { Binary.Operators.Plus, new Addition() },
                { Binary.Operators.Minus, new Subtraction() },
                { Binary.Operators.Asterisk, new Multiplication() },
                { Binary.Operators.Backslash, new Division() },
                { Binary.Operators.Percent, new Modulus() }
            };
        }

        public sealed override DataType DataType { get { return Primitives.Integer; } }

        private class Addition : Integer
        {
            public override short Evaluate(short left, short right)
            {
                return (short)(left + right);
            }
        }

        private class Subtraction : Integer
        {
            public override short Evaluate(short left, short right)
            {
                return (short)(left - right);
            }
        }

        private class Multiplication : Integer
        {
            public override short Evaluate(short left, short right)
            {
                return (short)(left * right);
            }
        }

        private class Division : Integer
        {
            public override short Evaluate(short left, short right)
            {
                return (short)(left / right);
            }
        }

        private class Modulus : Integer
        {
            public override short Evaluate(short left, short right)
            {
                return (short)(left % right);
            }
        }
    }
}
