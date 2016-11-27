using QBasic.Common;
using QBasic.Memory;
using QBasic.Program;
using QBasic.Program.Instructions;
using System;

namespace QBasic.Emulation
{
    public static class InstructionExecutor
    {
        private class Visitor : Visitor<Instruction>, Instruction.IVisitor
        {
            public Scope Scope { get; set; }

            public void Visit(Dim instruction)
            {
                throw new NotImplementedException();
            }

            public void Visit(Print instruction)
            {
                var value = ExpressionEvaluator.Evaluate(Scope, instruction.Argument);
                Console.WriteLine(value.Value.ToString());
            }
        }

        public static void Execute(Scope scope, Instruction instruction)
        {
            instruction.Accept(new Visitor() { Scope = scope });
        }
    }
}
