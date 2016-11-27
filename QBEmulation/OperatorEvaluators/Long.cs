using QBasic.Program.Expressions;
using QBasic.Types;
using System;
using System.Collections.Generic;

namespace QBasic.Emulation.OperatorEvaluators
{
    using OperatorSelector = IReadOnlyDictionary<Binary.Operators, DataTypeOperatorEvaluator>;

    abstract class Long : Primitive<Int32>
    {
        public static readonly OperatorSelector OperatorSelector;

        static Long()
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

        public sealed override DataType DataType { get { return Primitives.Long; } }

        private class Addition : Long
        {
            public override int Evaluate(int left, int right)
            {
                return (int)(left + right);
            }
        }

        private class Subtraction : Long
        {
            public override int Evaluate(int left, int right)
            {
                return (int)(left - right);
            }
        }

        private class Multiplication : Long
        {
            public override int Evaluate(int left, int right)
            {
                return (int)(left * right);
            }
        }

        private class Division : Long
        {
            public override int Evaluate(int left, int right)
            {
                return (int)(left / right);
            }
        }

        private class Modulus : Long
        {
            public override int Evaluate(int left, int right)
            {
                return (int)(left % right);
            }
        }
    }
}
