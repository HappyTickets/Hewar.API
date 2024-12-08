using System.Collections.Immutable;

namespace Application.Common.Utilities.Pagination
{
    public class PaginatedList<TData>
    {
        public IReadOnlyList<TData> Items { get; }

        public int TotalPages { get; }
        public int PageNumber { get; }
        public int PageSize { get; }

        public PaginatedList(IEnumerable<TData> items, int totalCount, int pageNumber, int pageSize)
        {
            Items = items.ToImmutableList();
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public bool HasPrevious => PageNumber > 0;
        public bool HasNext => PageNumber < TotalPages - 1;
    }
}
