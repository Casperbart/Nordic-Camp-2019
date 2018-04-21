using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> Get();
        Task<IEnumerable<INode<T>>> GetNodes(string after, int first);
        Task<IPageInfo> GetPageInfo(string after, int first);
        Task<T> Get(string cursor);
    }
}