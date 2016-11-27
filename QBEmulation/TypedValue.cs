using QBasic.Types;

namespace QBasic.Emulation
{
    public class TypedValue
    {
        public object Value { get; set; }

        public DataType DataType { get; set; }

        public override string ToString()
        {
            return DataType.ToString() + " : " + Value.ToString();
        }
    }
}
