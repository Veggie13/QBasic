namespace QBasic.Program.Instructions
{
    public class Print : Instruction
    {
        public Expression Argument { get; set; }

        public override string ToString()
        {
            return "PRINT " + Argument.ToString();
        }
    }
}
