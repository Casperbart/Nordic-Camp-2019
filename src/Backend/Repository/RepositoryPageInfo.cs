using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public class RepositoryPageInfo<T> : IPageInfo<T> where T : class
    {
        public RepositoryPageInfo(int totalCount, int page, bool hasNextPage, bool hasPrevPage, string startCursor, string endCursor)
        {
            TotalCount = totalCount;
            Page = page;
            HasNextPage = hasNextPage;
            HasPrevPage = hasPrevPage;
            StartCursor = startCursor ?? throw new ArgumentNullException(nameof(startCursor));
            EndCursor = endCursor ?? throw new ArgumentNullException(nameof(endCursor));
        }

        public int TotalCount { get; }
        public int Page { get; }
        public bool HasNextPage { get; }
        public bool HasPrevPage { get; }
        public string StartCursor { get; }
        public string EndCursor { get; }
    }
}
