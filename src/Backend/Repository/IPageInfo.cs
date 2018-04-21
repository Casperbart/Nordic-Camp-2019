namespace Backend.Repository
{
    /// <summary>
    /// Contains information about the current page in a connection query
    /// </summary>
    public interface IPageInfo
    {
        int TotalCount { get; }
        int Page { get; }
        bool HasNextPage { get; }
        bool HasPrevPage { get; }

        string StartCursor { get; }
        string EndCursor { get; }
    }
}