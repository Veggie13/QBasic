using QBasic.Types;
using System;
using System.Collections.Generic;

namespace QBasic.Emulation
{
    abstract class TypedValueConverter
    {
        private static Dictionary<DataType, TypedValueConverter> Conversion = new Dictionary<DataType, TypedValueConverter>();

        static TypedValueConverter()
        {
            Conversion[Primitives.String] = StringInstance = new StringTypedValueConverter();
            Conversion[Primitives.Double] = DoubleInstance = new DoubleTypedValueConverter();
            Conversion[Primitives.Single] = SingleInstance = new SingleTypedValueConverter();
            Conversion[Primitives.Long] = LongInstance = new LongTypedValueConverter();
            Conversion[Primitives.Integer] = IntegerInstance = new IntegerTypedValueConverter();
        }

        public static TypedValueConverter StringInstance { get; private set; }
        public static TypedValueConverter DoubleInstance { get; private set; }
        public static TypedValueConverter SingleInstance { get; private set; }
        public static TypedValueConverter LongInstance { get; private set; }
        public static TypedValueConverter IntegerInstance { get; private set; }

        public static TypedValueConverter Get(DataType type)
        {
            if (!Primitives.IsPrimitive(type))
            {
                throw new ArgumentException();
            }

            return Conversion[type];
        }

        public abstract DataType DataType { get; }

        public abstract TypedValue ConvertValue(TypedValue value);

        private class StringTypedValueConverter : TypedValueConverter
        {
            public override DataType DataType { get { return Primitives.String; } }

            public override TypedValue ConvertValue(TypedValue value)
            {
                if (value.DataType == DataType)
                {
                    return value;
                }
                if (!Primitives.IsPrimitive(value.DataType))
                {
                    throw new ArgumentException();
                }

                return new TypedValue()
                {
                    DataType = DataType,
                    Value = value.Value.ToString()
                };
            }
        }

        private class DoubleTypedValueConverter : TypedValueConverter
        {
            public override DataType DataType  { get { return Primitives.Double; } }

            public override TypedValue ConvertValue(TypedValue value)
            {
                if (value.DataType == DataType)
                {
                    return value;
                }
                if (value.DataType == Primitives.String)
                {
                    throw new ArgumentException();
                }
                if (!Primitives.IsPrimitive(value.DataType))
                {
                    throw new ArgumentException();
                }

                return new TypedValue()
                {
                    DataType = DataType,
                    Value = Convert.ToDouble(value.Value)
                };
            }
        }

        private class SingleTypedValueConverter : TypedValueConverter
        {
            public override DataType DataType { get { return Primitives.Single; } }

            public override TypedValue ConvertValue(TypedValue value)
            {
                if (value.DataType == DataType)
                {
                    return value;
                }
                if (value.DataType == Primitives.String)
                {
                    throw new ArgumentException();
                }
                if (!Primitives.IsPrimitive(value.DataType))
                {
                    throw new ArgumentException();
                }

                return new TypedValue()
                {
                    DataType = DataType,
                    Value = Convert.ToSingle(value.Value)
                };
            }
        }

        private class LongTypedValueConverter : TypedValueConverter
        {
            public override DataType DataType { get { return Primitives.Long; } }

            public override TypedValue ConvertValue(TypedValue value)
            {
                if (value.DataType == DataType)
                {
                    return value;
                }
                if (value.DataType == Primitives.String)
                {
                    throw new ArgumentException();
                }
                if (!Primitives.IsPrimitive(value.DataType))
                {
                    throw new ArgumentException();
                }

                return new TypedValue()
                {
                    DataType = DataType,
                    Value = Convert.ToInt32(value.Value)
                };
            }
        }

        private class IntegerTypedValueConverter : TypedValueConverter
        {
            public override DataType DataType { get { return Primitives.Integer; } }

            public override TypedValue ConvertValue(TypedValue value)
            {
                if (value.DataType == DataType)
                {
                    return value;
                }
                if (value.DataType == Primitives.String)
                {
                    throw new ArgumentException();
                }
                if (!Primitives.IsPrimitive(value.DataType))
                {
                    throw new ArgumentException();
                }

                return new TypedValue()
                {
                    DataType = DataType,
                    Value = Convert.ToInt16(value.Value)
                };
            }
        }
    }
}
