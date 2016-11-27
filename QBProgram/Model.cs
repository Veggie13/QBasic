using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBasic.Program
{
    public class Model
    {
        public Model(IEnumerable<Instruction> instructions)
        {
            Instructions = instructions.ToList();
        }

        public IEnumerable<Instruction> Instructions { get; private set; }
    }
}
