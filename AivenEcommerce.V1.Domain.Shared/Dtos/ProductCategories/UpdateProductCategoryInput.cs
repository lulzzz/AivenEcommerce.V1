
using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Shared.Dtos.ProductCategories
{
    public record UpdateProductCategoryInput(string OldName, string NewName, IEnumerable<string> SubCategories);

}
