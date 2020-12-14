﻿
using System;
using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Shared.Paginations
{
    public record PagedResult<T>(int CurrentPage, long TotalCount, long TotalPages, int PageSize, IEnumerable<T> Items) : Paged(CurrentPage, TotalCount, TotalPages, PageSize)
    {
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
    }
}
