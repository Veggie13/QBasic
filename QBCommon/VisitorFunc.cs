using System;

namespace QBasic.Common
{
    public abstract class VisitorFunc<TVisitable, T>
        where T : class
    {
        public T Result { get; set; }

        public void Visit(TVisitable visitable)
        {
            throw new NotImplementedException();
        }
    }

    public static partial class Extensions
    {
        public static void Evaluate<TVisitor, TVisitable, TVisitorFunc, T>(this VisitableBy<TVisitor, TVisitable> @this, TVisitorFunc visitor, out T result)
            where TVisitor : IVisitor<TVisitable>
            where TVisitable : VisitableBy<TVisitor, TVisitable>
            where TVisitorFunc : VisitorFunc<TVisitable, T>, TVisitor
            where T : class
        {
            @this.Accept(visitor);
            result = visitor.Result;
        }
    }
}