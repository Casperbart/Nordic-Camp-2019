using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Exceptions;
using Backend.Model;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.EF.Repository
{
    public abstract class EFBaseRepository<T, TKey> : IGenericRepository<T> where T : BaseEntity<TKey>
    {
        protected readonly ApplicationContext Context;

        public EFBaseRepository(ApplicationContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<T>> Get()
        {
            return await Context.Set<T>().ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<INode<T>>> GetNodes(string after, int first)
        {
            if (string.IsNullOrEmpty(after))
            {
                return await Context.Set<T>().OrderBy(e => e.Id).Take(first)
                    .Select(e => new RepositoryNode<T>(e.Id.ToString(), e)).ToListAsync().ConfigureAwait(false);
            }

            return await Context.Set<T>().OrderBy(e => e.Id).Where(e => e.Id.ToString().CompareTo(after) >= 0).Take(first)
                .Select(e => new RepositoryNode<T>(e.Id.ToString(), e)).ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<IPageInfo> GetPageInfo(string after, int first)
        {
            // Get start and end cursor
            var startCursor = await Context.Set<T>().OrderBy(e => e.Id).Select(e => e.Id).FirstAsync();
            var endCursor = await Context.Set<T>().OrderByDescending(e => e.Id).Select(e => e.Id).FirstAsync();

            // Get the total count and the count of the remaining items after the cursor
            var totalCount = await Context.Set<T>().CountAsync();
            int countUntilCursor = 0;
            if (!string.IsNullOrEmpty(after))
            {
                countUntilCursor = await Context.Set<T>().OrderBy(e => e.Id)
                    .Where(e => e.Id.ToString().CompareTo(after) < 0).CountAsync();
            }

            // Get the total skipped count of items
            var currentNumber = totalCount - countUntilCursor;

            // Get the current page number definde from the first parameter which defines the page size
            var page = currentNumber / first;

            // Check if has next or prev page
            var hasNextPage = (currentNumber - first) > 0;
            var hasPrevPage = countUntilCursor > 0;

            return new RepositoryPageInfo(totalCount: totalCount, page: page, hasNextPage: hasNextPage,
                hasPrevPage: hasPrevPage, startCursor: startCursor.ToString(), endCursor: endCursor.ToString());
        }

        /// <inheritdoc />
        public async Task<T> Get(string cursor)
        {
            // Get page and throw exception if page not found
            var page = await Context.Set<T>().SingleOrDefaultAsync(p => p.Id.ToString() == cursor).ConfigureAwait(false);
            if (page == null)
            {
                throw new ItemNotFoundException();
            }
            return page;
        }
    }
}