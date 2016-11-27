using QBasic.Program.Expressions;
using QBasic.Types;
using System;
using System.Collections.Generic;

namespace QBasic.Emulation.OperatorEvaluators
{
    using OperatorSelector = IReadOnlyDictionary<Binary.Operators, DataTypeOperatorEvaluator>;

    abstract class String : Primitive<string>
    {
        public static readonly OperatorSelector OperatorSelector;

        static String()
        {
            OperatorSelector = new Dictionary<Binary.Operators, DataTypeOperatorEvaluator>()
            {
                { Binary.Operators.Plus, new Concatenation() }
            };
        }

        public sealed override DataType DataType { get { return Primitives.String; } }

        private class Concatenation : String
        {
            public override string Evaluate(string left, string right)
            {
                return left + right;
            }
        }
    }
}
