namespace QBasic.Emulation
{
    public static class ProgramExecutor
    {
        public static void Execute(Program.Model program)
        {
            foreach (var instruction in program.Instructions)
            {
                //InstructionExecutor.Execute(instruction);
            }
        }
    }
}
