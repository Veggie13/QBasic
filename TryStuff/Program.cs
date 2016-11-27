using QBasic.Emulation;
using QBasic.Memory;
using QBasic.Parsing;
using System;

namespace TryStuff
{
    class Program
    {
        static void Main(string[] args)
        {
            //var expression = ExpressionParser.Parse("  ( \"asdag (asdg(asdg ))fa \"  +  \"asgasdga\"  )  +  \" \"   ");
            var block = new Block(UInt16.MaxValue);
            var stack = new Stack(block);
            var scope = new Scope(stack);
            scope.Add16Variable("testVar%").Set((short)13);
            var expression = ExpressionParser.Parse(@"testVar%");
            var val = ExpressionEvaluator.Evaluate(scope, expression);
            //var instruction = InstructionParser.Parse("  PRINT  ( \"asdag (asdg(asdg ))fa \"  +  \"asgasdga\"  )  +  \" \"   ");
            //InstructionExecutor.Execute(instruction);
            Console.ReadLine();
        }
    }
}
