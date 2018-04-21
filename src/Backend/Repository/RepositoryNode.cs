using System;

namespace Backend.Repository
{
    /// <inheritdoc />
    public class RepositoryNode<T> : INode<T> where T : class
    {
        /// <summary>
        /// Initilizes a new Repository node with the specified <paramref name="cursor"/> and <paramref name="node"/>
        /// </summary>
        /// <param name="cursor">The cursor for the node</param>
        /// <param name="node">The node</param>
        public RepositoryNode(string cursor, T node)
        {
            Cursor = cursor ?? throw new ArgumentNullException(nameof(cursor));
            Node = node ?? throw new ArgumentNullException(nameof(node));
        }
        
        /// <inheritdoc />
        public string Cursor { get; }

        /// <inheritdoc />
        public T Node { get; }
    }
}