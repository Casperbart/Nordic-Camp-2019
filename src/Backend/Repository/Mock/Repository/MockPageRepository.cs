using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Exceptions;
using Backend.Model;

namespace Backend.Repository.Mock.Repository
{
    public class MockPageRepository : IPageRepository
    {
        private List<Page> _pages = new List<Page>()
        {
            new Page
            {
                Url = "About",
                Content = "# About Nordic 4H Camp\nWork in progress"
            }
        };

        public Task<IEnumerable<Page>> Get()
        {
            return Task.FromResult((IEnumerable<Page>)_pages.AsReadOnly());
        }

        public async Task<IEnumerable<INode<Page>>> GetNodes(string after, int first)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<IPageInfo<Page>>> GetPageInfo(string after, int first)
        {
            throw new NotImplementedException();
        }

        public Task<Page> Get(string cursor)
        {
            var page = _pages.FirstOrDefault(e => string.Equals(e.Url, cursor, StringComparison.CurrentCultureIgnoreCase));
            if (page == null)
            {
                throw new PageNotFoundException();
            }

            return Task.FromResult(page);
        }
    }
}
