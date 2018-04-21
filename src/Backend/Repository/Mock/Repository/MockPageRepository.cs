using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Exceptions;
using Backend.Model;

namespace Backend.Repository.Mock.Repository
{
    /// <inheritdoc />
    public class MockPageRepository : MockBaseRepository<Page>, IPageRepository
    {
        /// <inheritdoc />
        public override Task<List<Page>> GetInitialData()
        {
            return Task.FromResult(new List<Page>()
            {
                new Page
                {
                    Url = "About",
                    Content = "# About Nordic 4H Camp\nWork in progress"
                }
            });
        }

        /// <inheritdoc />
        public override string GetCursor(Page item)
        {
            return item.Url;
        }

        /// <inheritdoc />
        public async Task<Page> AddPage(string url, string content)
        {
            // Get data
            var mockData = await GetData();

            // Check if page already exists
            if (mockData.Any(p => p.Url == url))
            {
                throw new ItemAlreadyExistsException();
            }
            
            // Add page
            var page = new Page { Url = url, Content = content };
            mockData.Add(page);
            return page;
        }

        /// <inheritdoc />
        public async Task<Page> EditPage(string url, string content)
        {
            // Get data
            var mockData = await GetData();

            // Get page
            var page = mockData.FirstOrDefault(p => p.Url == url);
            if (page == null)
            {
                throw new ItemNotFoundException();
            }

            // Update content
            page.Content = content;

            return page;
        }

        /// <inheritdoc />
        public async Task DeletePage(string url)
        {
            // Get data
            var mockData = await GetData();

            // Get page
            var page = mockData.FirstOrDefault(p => p.Url == url);
            if (page == null)
            {
                throw new ItemNotFoundException();
            }
        }
    }
}
