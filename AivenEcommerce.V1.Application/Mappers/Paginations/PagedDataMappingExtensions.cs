﻿using AivenEcommerce.V1.Domain.Shared.Paginations;

using System;
using System.Linq;

namespace AivenEcommerce.V1.Application.Mappers.Paginations
{
    public static class PagedDataMappingExtensions
    {
        public static PagedData<TDto> ConvertToDto<TEntity, TDto>(this PagedData<TEntity> data, Func<TEntity, TDto> selector)
            => new(data.Items.Select(selector), data.TotalCount);

    }
}
