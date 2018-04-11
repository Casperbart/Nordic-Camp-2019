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

        public Task<IEnumerable<INode<Page>>> GetNodes(string after, int first)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IPageInfo<Page>>> GetPageInfo(string after, int first)
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

        public Task<Page> AddPage(string url, string content)
        {
            // Check if page already exists
            if (_pages.Any(p => p.Url == url))
            {
                throw new PageAlreadyExistsException();
            }
            
            // Add page
            var page = new Page { Url = url, Content = content };
            _pages.Add(page);
            return Task.FromResult(page);
        }

        public Task<Page> EditPage(string url, string content)
        {
            // Get page
            var page = _pages.FirstOrDefault(p => p.Url == url);
            if (page == null)
            {
                throw new PageNotFoundException();
            }

            // Update content
            page.Content = content;

            return Task.FromResult(page);
        }

        public Task DeletePage(string url)
        {
            // Get page
            var page = _pages.FirstOrDefault(p => p.Url == url);
            if (page == null)
            {
                throw new PageNotFoundException();
            }

            return Task.FromResult(true);
        }
    }
}
