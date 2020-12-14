
using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Shared.Paginations
{
    public record PagedData<T>(IEnumerable<T> Items, long TotalCount);
}
