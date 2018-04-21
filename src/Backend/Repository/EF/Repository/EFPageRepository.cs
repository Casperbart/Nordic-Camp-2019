using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Exceptions;
using Backend.Model;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.EF.Repository
{
    /// <inheritdoc />
    public class EfPageRepository : EFBaseRepository<Page, string>, IPageRepository
    {
        public EfPageRepository(ApplicationContext context) : base(context)
        {
        }
        
        /// <inheritdoc />
        public async Task<Page> AddPage(string url, string content)
        {
            // TODO: Handle dublicate page with custom exception
            var page = new Page {Url = url, Content = content};
            Context.Add(page);
            await Context.SaveChangesAsync().ConfigureAwait(false);
            return page;
        }

        /// <inheritdoc />
        public async Task<Page> EditPage(string url, string content)
        {
            // Get page and update content
            var page = await Context.Set<Page>().SingleOrDefaultAsync(e => e.Url == url).ConfigureAwait(true);
            if (page == null)
            {
                throw new ItemNotFoundException();
            }
            page.Content = content;
            await Context.SaveChangesAsync().ConfigureAwait(false);
            return page;
        }

        /// <inheritdoc />
        public async Task DeletePage(string url)
        {
            // Get page and delete
            var page = await Context.Set<Page>().SingleOrDefaultAsync(e => e.Url == url).ConfigureAwait(true);
            if (page == null)
            {
                throw new ItemNotFoundException();
            }
            Context.Set<Page>().Remove(page);
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
