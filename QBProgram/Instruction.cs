using QBasic.Common;

namespace QBasic.Program
{
    public abstract class Instruction : VisitableBy<Instruction.IVisitor, Instruction>
    {
        public interface IVisitor : IVisitor<Instruction>
        {
            void Visit(Instructions.Dim instruction);
            void Visit(Instructions.Print instruction);
        }

        public abstract override string ToString();
    }
}
