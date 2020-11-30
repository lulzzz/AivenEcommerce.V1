
using System;
using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Shared.Paginations
{
    public class PagedResult<T> : Paged
    {
        public PagedResult(PagedData<T> data, QueryStringParameters parameters)
        {
            PageSize = parameters.PageSize ?? Convert.ToInt32(data.TotalCount);
            CurrentPage = parameters.PageNumber;
            TotalCount = data.TotalCount;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
            Items = data.Items;
        }

        public IEnumerable<T> Items { get; set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

    }
}
