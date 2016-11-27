using QBasic.Common;

namespace QBasic.Program
{
    public abstract class Expression : VisitableBy<Expression.IVisitor, Expression>
    {
        public interface IVisitor : IVisitor<Expression>
        {
            void Visit(Expressions.Constant expression);
            void Visit(Expressions.Binary expression);
            void Visit(Expressions.Variable expression);
        }

        public abstract override string ToString();
    }
}
