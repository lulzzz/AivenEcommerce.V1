using System;
using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Dtos.ProductCategories
{
    public record ProductCategoryDto(Guid Id, string Name, int ProductCount, IEnumerable<string> SubCategories);
}
