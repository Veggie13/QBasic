using System.Collections.Generic;

namespace QBasic.Memory
{
    public class Scope
    {
        private Stack _stack;
        private Dictionary<string, Variable> _variables = new Dictionary<string, Variable>();

        public Scope(Stack stack)
        {
            _stack = stack;
        }

        public Variable this[string name]
        {
            get { return _variables[name]; }
        }

        public Variable Add16Variable(string name)
        {
            var variable = new Variable(_stack.Block, name, _stack.Push16());
            _variables[name] = variable;
            return variable;
        }

        public Variable Add32Variable(string name)
        {
            var variable = new Variable(_stack.Block, name, _stack.Push32());
            _variables[name] = variable;
            return variable;
        }

        public Variable Add64Variable(string name)
        {
            var variable = new Variable(_stack.Block, name, _stack.Push64());
            _variables[name] = variable;
            return variable;
        }
    }
}
