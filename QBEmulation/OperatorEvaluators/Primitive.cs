namespace QBasic.Emulation.OperatorEvaluators
{
    abstract class Primitive<T> : DataTypeOperatorEvaluator
    {
        public sealed override TypedValue Evaluate(TypedValue left, TypedValue right)
        {
            left = TypedValueConverter.Get(DataType).ConvertValue(left);
            right = TypedValueConverter.Get(DataType).ConvertValue(right);

            return new TypedValue()
            {
                DataType = DataType,
                Value = Evaluate((T)left.Value, (T)right.Value)
            };
        }

        public abstract T Evaluate(T left, T right);
    }
}