namespace QBasic.Common
{
    public interface IVisitor<TVisitable>
    {
        void Visit(TVisitable visitable);
    }
}