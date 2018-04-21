using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Exceptions;

namespace Backend.Repository.Mock.Repository
{
    public abstract class MockBaseRepository<T> : IGenericRepository<T> where T : class
    {
        private List<T> _mockData;
        
        /// <inheritdoc />
        public async Task<IEnumerable<T>> Get()
        {
            return (await GetData().ConfigureAwait(false)).AsReadOnly();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<INode<T>>> GetNodes(string after, int first)
        {
            if (string.IsNullOrEmpty(after))
            {
                return (await GetData()).OrderBy(GetCursor).Take(first)
                    .Select(e => new RepositoryNode<T>(GetCursor(e), e)).ToList().AsReadOnly();
            }

            return (await GetData()).OrderBy(GetCursor).Where(e => GetCursor(e).CompareTo(after) >= 0).Take(first)
                .Select(e => new RepositoryNode<T>(GetCursor(e), e)).ToList().AsReadOnly();
        }

        /// <inheritdoc />
        public async Task<IPageInfo<T>> GetPageInfo(string after, int first)
        {
            // Get start and end cursor
            var startCursor = (await GetData()).OrderBy(GetCursor).Select(GetCursor).First();
            var endCursor = (await GetData()).OrderByDescending(GetCursor).Select(GetCursor).First();

            // Get the total count and the count of the remaining items after the cursor
            var totalCount = (await GetData()).Count;
            int countUntilCursor = 0;
            if (!string.IsNullOrEmpty(after))
            {
                countUntilCursor = (await GetData()).OrderBy(e => GetCursor(e))
                    .Where(e => GetCursor(e).CompareTo(after) < 0).Count();
            }

            // Get the total skipped count of items
            var currentNumber = totalCount - countUntilCursor;

            // Get the current page number definde from the first parameter which defines the page size
            var page = currentNumber / first;

            // Check if has next or prev page
            var hasNextPage = (currentNumber - first) > 0;
            var hasPrevPage = countUntilCursor > 0;

            return new RepositoryPageInfo<T>(totalCount: totalCount, page: page, hasNextPage: hasNextPage,
                hasPrevPage: hasPrevPage, startCursor: startCursor.ToString(), endCursor: endCursor.ToString());
        }

        /// <inheritdoc />
        public async Task<T> Get(string cursor)
        {
            var page = (await GetData().ConfigureAwait(false)).FirstOrDefault(item => string.Equals(GetCursor(item), cursor, StringComparison.CurrentCultureIgnoreCase));
            if (page == null)
            {
                throw new ItemNotFoundException();
            }

            return page;
        }

        protected async Task<List<T>> GetData()
        {
            if (_mockData == null)
                _mockData = await GetInitialData().ConfigureAwait(false);

            return _mockData;
        }

        public abstract Task<List<T>> GetInitialData();

        public abstract string GetCursor(T item);
    }
}