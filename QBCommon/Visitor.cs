using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBasic.Common
{
    public abstract class Visitor<TVisitable> : IVisitor<TVisitable>
    {
        public void Visit(TVisitable visitable)
        {
            throw new NotImplementedException();
        }
    }
}
