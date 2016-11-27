using System;

namespace QBasic.Common
{
    public abstract class VisitableBy<TVisitor, TThis>
        where TVisitor : IVisitor<TThis>
        where TThis : VisitableBy<TVisitor, TThis>
    {
        public void Accept(TVisitor visitor)
        {
            visitor.Visit((dynamic)this);
        }
    }
}
