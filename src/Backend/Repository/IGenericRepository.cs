using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Exceptions;

namespace Backend.Repository
{
    /// <summary>
    /// Generic repository which supports getting data
    /// </summary>
    /// <typeparam name="T">The type of the data to request</typeparam>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Get all data in the repository
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> with all the data in the repository</returns>
        Task<IEnumerable<T>> Get();
        /// <summary>
        /// Get all nodes after the specific key (<paramref name="after"/>) and the specified number of elements in parameter <paramref name="first"/>
        /// </summary>
        /// <param name="after">The first item to request</param>
        /// <param name="first">The number of items to request</param>
        /// <returns>An <see cref="IEnumerable{T}"/> with the number (or less) nodes requested with the parameter <paramref name="first"/> starting from the item <paramref name="after"/></returns>
        Task<IEnumerable<INode<T>>> GetNodes(string after, int first);

        /// <summary>
        /// Returns page info about the items in the GraphQL connection
        /// </summary>
        /// <param name="after">The first item to request</param>
        /// <param name="first">The number of items to request</param>
        /// <returns>Returns page info such as if there are more pages, the first and last cursor etc.</returns>
        Task<IPageInfo> GetPageInfo(string after, int first);

        /// <summary>
        /// Gets a item from the repository matching the specific cursor
        /// </summary>
        /// <param name="cursor">The cursor to match</param>
        /// <returns>Returns the item if found</returns>
        /// <exception cref="ItemNotFoundException">Throws if the item matching the parameter <paramref name="cursor"/> could not be found</exception>
        Task<T> Get(string cursor);
    }
}