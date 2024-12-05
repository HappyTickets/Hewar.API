namespace Application.Common.Utilities
{
    public class PaginatedList<TData>
    {
        public IReadOnlyList<TData> Items { get; }

        public int TotalPages { get; }
        public int PageNumber { get; }
        public int PageSize { get; }

        public PaginatedList(IReadOnlyList<TData> items, int totalCount, int pageNumber, int pageSize)
        {
            Items = items;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public bool HasNextPage => PageNumber < TotalPages;
        public bool HasPreviousPage => PageNumber > 1;
    }
}
