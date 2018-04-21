using System.Threading.Tasks;
using Backend.Exceptions;
using Backend.Model;

namespace Backend.Repository
{
    /// <summary>
    /// Contains methods for accessing the pages
    /// </summary>
    public interface IPageRepository : IGenericRepository<Page>
    {
        /// <summary>
        /// Adds a page
        /// </summary>
        /// <param name="url">The url of the new page</param>
        /// <param name="content">The content of the page</param>
        /// <returns>The newly created page</returns>
        /// <exception cref="ItemAlreadyExistsException">Throws if the url is already present in the repository</exception>
        Task<Page> AddPage(string url, string content);

        /// <summary>
        /// Edits a page
        /// </summary>
        /// <param name="url">The url of the page to change</param>
        /// <param name="content">The new content of the page</param>
        /// <returns>The updated page</returns>
        /// <exception cref="ItemNotFoundException">Throws if the page was not found</exception>
        Task<Page> EditPage(string url, string content);

        /// <summary>
        /// Deletes a page
        /// </summary>
        /// <param name="url">The url of the page to delete</param>
        /// <returns></returns>
        /// <exception cref="ItemNotFoundException">Throws if the page was not found</exception>
        Task DeletePage(string url);
    }
}
