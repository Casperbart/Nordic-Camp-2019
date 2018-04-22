using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Repository
{
    /// <inheritdoc />
    public class RepositoryPageInfo : IPageInfo
    {
        /// <summary>
        /// Initilizes a new PageInfo using the specified parameters
        /// </summary>
        /// <param name="totalCount">The total count of elements</param>
        /// <param name="page">The current page number</param>
        /// <param name="hasNextPage">True if there are more pages</param>
        /// <param name="hasPrevPage">True if there are previous pages</param>
        /// <param name="startCursor">The start cursor of this request</param>
        /// <param name="endCursor">The last cursor in the connection</param>
        public RepositoryPageInfo(int totalCount, int page, bool hasNextPage, bool hasPrevPage, string startCursor, string endCursor)
        {
            TotalCount = totalCount;
            Page = page;
            HasNextPage = hasNextPage;
            HasPrevPage = hasPrevPage;
            StartCursor = startCursor ?? throw new ArgumentNullException(nameof(startCursor));
            EndCursor = endCursor ?? throw new ArgumentNullException(nameof(endCursor));
        }

        /// <inheritdoc />
        public int TotalCount { get; }
        /// <inheritdoc />
        public int Page { get; }
        /// <inheritdoc />
        public bool HasNextPage { get; }
        /// <inheritdoc />
        public bool HasPrevPage { get; }
        /// <inheritdoc />
        public string StartCursor { get; }
        /// <inheritdoc />
        public string EndCursor { get; }
    }
}
