using System;

namespace Backend.Repository
{
    public class RepositoryNode<T> : INode<T> where T : class
    {
        public RepositoryNode(string cursor, T node)
        {
            Cursor = cursor ?? throw new ArgumentNullException(nameof(cursor));
            Node = node ?? throw new ArgumentNullException(nameof(node));
        }

        public string Cursor { get; }
        public T Node { get; }
    }
}