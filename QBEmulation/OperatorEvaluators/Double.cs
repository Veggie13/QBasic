using QBasic.Program.Expressions;
using QBasic.Types;
using System.Collections.Generic;

namespace QBasic.Emulation.OperatorEvaluators
{
    using OperatorSelector = IReadOnlyDictionary<Binary.Operators, DataTypeOperatorEvaluator>;

    abstract class Double : Primitive<double>
    {
        public static readonly OperatorSelector OperatorSelector;

        static Double()
        {
            OperatorSelector = new Dictionary<Binary.Operators, DataTypeOperatorEvaluator>()
            {
                { Binary.Operators.Plus, new Addition() },
                { Binary.Operators.Minus, new Subtraction() },
                { Binary.Operators.Asterisk, new Multiplication() },
                { Binary.Operators.Slash, new Division() }
            };
        }

        public sealed override DataType DataType { get { return Primitives.Double; } }

        private class Addition : Double
        {
            public override double Evaluate(double left, double right)
            {
                return left + right;
            }
        }

        private class Subtraction : Double
        {
            public override double Evaluate(double left, double right)
            {
                return left - right;
            }
        }

        private class Multiplication : Double
        {
            public override double Evaluate(double left, double right)
            {
                return left * right;
            }
        }

        private class Division : Double
        {
            public override double Evaluate(double left, double right)
            {
                return left / right;
            }
        }
    }
}
