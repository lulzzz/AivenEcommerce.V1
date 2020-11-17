using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Dtos.ProductVariants
{
    public record DeleteProductVariantInput(string ProductId, string Name);
}
