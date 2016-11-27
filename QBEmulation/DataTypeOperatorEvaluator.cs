using QBasic.Types;
using QBasic.Program.Expressions;
using System;
using System.Collections.Generic;

namespace QBasic.Emulation
{
    using OperatorSelector = IReadOnlyDictionary<Binary.Operators, DataTypeOperatorEvaluator>;

    abstract class DataTypeOperatorEvaluator
    {
        private static readonly IReadOnlyDictionary<DataType, OperatorSelector> Selectors;

        static DataTypeOperatorEvaluator()
        {
            Selectors = new Dictionary<DataType, OperatorSelector>()
            {
                { Primitives.String, OperatorEvaluators.String.OperatorSelector },
                { Primitives.Double, OperatorEvaluators.Double.OperatorSelector },
                { Primitives.Single, OperatorEvaluators.Single.OperatorSelector },
                { Primitives.Long, OperatorEvaluators.Long.OperatorSelector },
                { Primitives.Integer, OperatorEvaluators.Integer.OperatorSelector }
            };
        }

        public static DataTypeOperatorEvaluator Get(DataType type, Binary.Operators op)
        {
            if (!Selectors.ContainsKey(type))
            {
                throw new NotImplementedException();
            }
            if (!Selectors[type].ContainsKey(op))
            {
                return null;
            }

            return Selectors[type][op];
        }

        public abstract DataType DataType { get; }

        public abstract TypedValue Evaluate(TypedValue left, TypedValue right);
    }
}
