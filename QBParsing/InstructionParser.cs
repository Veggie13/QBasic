using QBasic.Program;
using QBasic.Program.Instructions;
using System;
using System.Text.RegularExpressions;

namespace QBasic.Parsing
{
    public static class InstructionParser
    {
        private static readonly Regex PrintRegex = new Regex(@"^PRINT[\s]+(.*)$");

        public static Instruction Parse(string instructionString)
        {
            instructionString = instructionString.Trim();

            if (PrintRegex.IsMatch(instructionString))
            {
                var match = PrintRegex.Match(instructionString);
                return new Print()
                {
                    Argument = ExpressionParser.Parse(match.Groups[1].Value)
                };
            }

            throw new NotImplementedException();
        }
    }
}
