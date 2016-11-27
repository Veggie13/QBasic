using QBasic.Program.Expressions;
using System.Collections.Generic;

namespace QBasic.Emulation.OperatorEvaluators
{
    using Types;
    using OperatorSelector = IReadOnlyDictionary<Binary.Operators, DataTypeOperatorEvaluator>;

    abstract class Single : Primitive<float>
    {
        public static readonly OperatorSelector OperatorSelector;

        static Single()
        {
            OperatorSelector = new Dictionary<Binary.Operators, DataTypeOperatorEvaluator>()
            {
                { Binary.Operators.Plus, new Addition() },
                { Binary.Operators.Minus, new Subtraction() },
                { Binary.Operators.Asterisk, new Multiplication() },
                { Binary.Operators.Slash, new Division() }
            };
        }

        public sealed override DataType DataType { get { return Primitives.Single; } }

        private class Addition : Single
        {
            public override float Evaluate(float left, float right)
            {
                return left + right;
            }
        }

        private class Subtraction : Single
        {
            public override float Evaluate(float left, float right)
            {
                return left - right;
            }
        }

        private class Multiplication : Single
        {
            public override float Evaluate(float left, float right)
            {
                return left * right;
            }
        }

        private class Division : Single
        {
            public override float Evaluate(float left, float right)
            {
                return left / right;
            }
        }
    }
}
