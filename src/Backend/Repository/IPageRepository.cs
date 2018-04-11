using System.Threading.Tasks;
using Backend.Model;

namespace Backend.Repository
{
    public interface IPageRepository : IGenericRepository<Page>
    {
        Task<Page> AddPage(string url, string content);
        Task<Page> EditPage(string url, string content);
        Task<Page> DeletePage(string url, string content);
    }
}
