using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Shared.Paginations
{
    public record PagedResult<T>(int CurrentPage, long TotalCount, long TotalPages, int PageSize, IEnumerable<T> Items)
    {
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
    }
}


//public class PagedResult<T>
//{
//    public int CurrentPage { get; init; }
//    public long TotalCount { get; init; }
//    public long TotalPages { get; init; }
//    public int PageSize { get; init; }
//    public IEnumerable<T> Items { get; init; }
//    public bool HasPrevious => CurrentPage > 1;
//    public bool HasNext => CurrentPage < TotalPages;
//}