
using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Shared.Dtos.ProductCategories
{
    public record CreateProductCategoryInput(string Name, IEnumerable<string> SubCategories);
}
