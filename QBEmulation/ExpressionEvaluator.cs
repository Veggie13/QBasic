using QBasic.Common;
using QBasic.Memory;
using QBasic.Program;
using QBasic.Program.Expressions;
using QBasic.Types;
using System;

namespace QBasic.Emulation
{
    public static class ExpressionEvaluator
    {
        private class Visitor : VisitorFunc<Expression, TypedValue>, Expression.IVisitor
        {
            public Scope Scope { get; set; }

            public void Visit(Constant expression)
            {
                Result = new TypedValue()
                {
                    DataType = expression.DataType,
                    Value = expression.Value
                };
            }

            public void Visit(Binary expression)
            {
                var left = Evaluate(Scope, expression.Left);
                var right = Evaluate(Scope, expression.Right);

                var specificType = DataType.GetMostSpecific(left.DataType, right.DataType);

                DataTypeOperatorEvaluator evaluator = null;
                while (evaluator == null && specificType != null)
                {
                    evaluator = DataTypeOperatorEvaluator.Get(specificType, expression.Operator);
                    specificType = DataType.GetNextMostSpecific(specificType);
                }

                Result = evaluator.Evaluate(left, right);
            }

            public void Visit(Program.Expressions.Variable expression)
            {
                var variable = Scope[expression.Name];
                var value = new TypedValue()
                {
                    DataType = expression.DataType
                };

                if (expression.DataType == Primitives.Integer)
                {
                    value.Value = variable.GetInt16();
                }
                else if (expression.DataType == Primitives.Long)
                {
                    value.Value = variable.GetInt32();
                }
                else if (expression.DataType == Primitives.Single)
                {
                    value.Value = variable.GetSingle();
                }
                else if (expression.DataType == Primitives.Double)
                {
                    value.Value = variable.GetDouble();
                }
                else
                {
                    throw new NotImplementedException();
                }

                Result = value;
            }
        }

        public static TypedValue Evaluate(Scope scope, Expression expression)
        {
            TypedValue result;
            expression.Evaluate(new Visitor() { Scope = scope }, out result);
            return result;
        }
    }
}
