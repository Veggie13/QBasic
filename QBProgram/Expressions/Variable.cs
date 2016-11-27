using QBasic.Types;

namespace QBasic.Program.Expressions
{
    public class Variable : Expression
    {
        public string Name { get; set; }

        public DataType DataType { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
