
using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Shared.Paginations
{
    public class PagedData<T>
    {
        public IEnumerable<T> Items { get; set; }
        public long TotalCount { get; set; }
    }
}
