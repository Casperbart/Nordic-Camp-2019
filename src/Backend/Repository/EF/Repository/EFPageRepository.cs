using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Page>> GetPages()
        {
            return await _context.Pages.ToListAsync().ConfigureAwait(false);
        }

        public async Task<Page> GetPage(string url)
        {
            var lowerCaseUrl = url.ToLower();
            return await _context.Pages.SingleOrDefaultAsync(page => page.Url == lowerCaseUrl).ConfigureAwait(false);
        }
    }
}
