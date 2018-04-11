namespace Backend.Repository
{
    public interface IPageInfo<T>
        where T : class
    {
        int TotalCount { get; }
        int Page { get; }
        bool HasNextPage { get; }

        string StartCursor { get; }
        string EndCursor { get; }
    }
}