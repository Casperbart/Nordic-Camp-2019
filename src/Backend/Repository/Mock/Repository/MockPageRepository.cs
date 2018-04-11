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

        public Task<IEnumerable<Page>> GetPages()
        {
            return Task.FromResult((IEnumerable<Page>)_pages.AsReadOnly());
        }

        public Task<Page> GetPage(string url)
        {
            var page = _pages.FirstOrDefault(e => string.Equals(e.Url, url, StringComparison.CurrentCultureIgnoreCase));
            if (page == null)
            {
                throw new PageNotFoundException();
            }

            return Task.FromResult(page);
        }
    }
}
