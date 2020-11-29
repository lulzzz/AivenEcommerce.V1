using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Dtos.ProductVariants
{
    public record ProductVariantLiteDto(string Name, IEnumerable<string> Values);

}
