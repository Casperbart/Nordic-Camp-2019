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
        /// <inheritdoc />
        public EfPageRepository(ApplicationContext context) : base(context)
        {
        }
        
        /// <inheritdoc />
        public async Task<Page> AddPage(string url, string content)
        {
            try
            {
                var page = new Page {Url = url, Content = content};
                Context.Add(page);
                await Context.SaveChangesAsync().ConfigureAwait(false);
                return page;
            }
            catch (DbUpdateException updateException)
            {
                // Handle update exceptions
                // It could here be the case that the page already exists
                // We check this by quering the database
                try
                {
                    // Get the page
                    var page = await Get(url);

                    // Page was found and therefore it's a dublicate
                    if (page != null)
                    {
                        throw new ItemAlreadyExistsException(updateException);
                    }
                }
                catch (ItemNotFoundException)
                {
                    // The item was not found and is therefore possible a serious error
                    // We want therefore to throw the original exception and therefore fall out
                    // If we throw here we just throw the ItemNotFoundException
                }
                catch (ItemAlreadyExistsException)
                {
                    // Thrown when the page already exists
                    throw;
                }
                catch (Exception ex)
                {
                    // Return both exceptions
                    throw new AggregateException(updateException, ex);
                }

                throw;
            }
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
