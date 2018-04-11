namespace Backend.Repository
{
    public interface INode<T>
        where T : class
    {
        string Cursor { get; }
        T Node { get; }
    }
}