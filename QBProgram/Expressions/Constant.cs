using QBasic.Types;

namespace QBasic.Program.Expressions
{
    public class Constant : Expression
    {
        public DataType DataType { get; set; }

        public object Value { get; set; }

        public override string ToString()
        {
            if (DataType == Primitives.String)
            {
                return "\"" + Value.ToString() + "\"";
            }
            if (DataType == Primitives.Double)
            {
                return Value.ToString() + "#";
            }
            if (DataType == Primitives.Integer)
            {
                return Value.ToString() + "%";
            }
            if (DataType == Primitives.Long)
            {
                return Value.ToString() + "&";
            }
            if (DataType == Primitives.Single)
            {
                return Value.ToString() + "!";
            }
            return DataType.ToString();
        }
    }
}
