using AivenEcommerce.V1.Domain.Shared.Paginations;

using System;

namespace AivenEcommerce.V1.Application.Extensions
{
    public static class PaginationExtensions
    {
        public static int? CalculateSkip(this QueryStringParameters parameters)
        {
            return (parameters.PageNumber - 1) * parameters.PageSize;
        }

        public static PagedResult<T> ConvertToPagedResult<T>(this PagedData<T> data, QueryStringParameters parameters)
        {
            int pageSize = parameters.PageSize ?? Convert.ToInt32(data.TotalCount);

            return new(
                    parameters.PageNumber,
                    data.TotalCount,
                    (int)Math.Ceiling(data.TotalCount / (double)pageSize),
                    pageSize,
                    data.Items
                );
        }
    }
}
