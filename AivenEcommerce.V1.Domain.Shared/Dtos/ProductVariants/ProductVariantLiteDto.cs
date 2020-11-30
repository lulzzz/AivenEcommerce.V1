
using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Shared.Dtos.ProductVariants
{
    public record ProductVariantLiteDto(string Name, IEnumerable<string> Values);

}
