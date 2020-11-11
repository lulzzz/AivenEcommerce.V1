using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Dtos.ProductCategories
{
    public record CreateProductCategoryInput(string Name, IEnumerable<string> SubCategories);
}
