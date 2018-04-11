using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.EF.Repository
{
    public class EfPageRepository : IPageRepository
    {
        private readonly ApplicationContext _context;

        public EfPageRepository(ApplicationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Page>> Get()
        {
            return await _context.Pages.ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<INode<Page>>> GetNodes(string after, int first)
        {
            return await _context.Pages.OrderBy(e => e.Url).SkipWhile(e => e.Url != after).Take(first)
                .Select(e => new EFNode<Page>(e.Url, e)).ToListAsync().ConfigureAwait(false);
        }

        public Task<IEnumerable<IPageInfo<Page>>> GetPageInfo(string after, int first)
        {
            throw new NotImplementedException();
        }

        public async Task<Page> Get(string cursor)
        {
            var lowerCaseUrl = cursor.ToLower();
            return await _context.Pages.SingleOrDefaultAsync(page => page.Url == lowerCaseUrl).ConfigureAwait(false);
        }

        public async Task<Page> AddPage(string url, string content)
        {
            // TODO: Handle dublicate page with custom exception
            var page = new Page {Url = url, Content = content};
            _context.Add(page);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return page;
        }

        public async Task<Page> EditPage(string url, string content)
        {
            throw new NotImplementedException();
        }

        public async Task<Page> DeletePage(string url, string content)
        {
            throw new NotImplementedException();
        }

        private class EFNode<T> : INode<T> where T : class
        {
            public EFNode(string cursor, T node)
            {
                Cursor = cursor ?? throw new ArgumentNullException(nameof(cursor));
                Node = node ?? throw new ArgumentNullException(nameof(node));
            }

            public string Cursor { get; }
            public T Node { get; }
        }
    }
}
