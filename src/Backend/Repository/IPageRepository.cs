using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Model;

namespace Backend.Repository
{
    public interface IPageRepository : IGenericRepository<Page>
    {
    }

    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> Get();
        Task<IEnumerable<INode<T>>> GetNodes(string after, int first);
        Task<IEnumerable<IPageInfo<T>>> GetPageInfo(string after, int first);
        Task<T> Get(string cursor);
    }

    public interface INode<T>
        where T : class
    {
        string Cursor { get; }
        T Node { get; }
    }

    public interface IPageInfo<T>
        where T : class
    {
        int TotalCount { get; }
        int Page { get; }
        bool HasNextPage { get; }

        string StartCursor { get; }
        string EndCursor { get; }
    }
}
