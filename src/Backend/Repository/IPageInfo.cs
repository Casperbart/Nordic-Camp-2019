namespace Backend.Repository
{
    /// <summary>
    /// Contains information about the current page in a connection query
    /// </summary>
    public interface IPageInfo
    {
        /// <summary>
        /// The total number of items in the connection
        /// </summary>
        int TotalCount { get; }

        /// <summary>
        /// The current page number
        /// </summary>
        int Page { get; }

        /// <summary>
        /// True if the connection has a next page
        /// </summary>
        bool HasNextPage { get; }

        /// <summary>
        /// True if the connection has a previous page
        /// </summary>
        bool HasPrevPage { get; }

        /// <summary>
        /// The first item in the connection
        /// </summary>
        string StartCursor { get; }

        /// <summary>
        /// The last item in the connection
        /// </summary>
        string EndCursor { get; }
    }
}