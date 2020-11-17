using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Dtos.ProductVariants;
using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Application.Mappers.ProductVariants
{
    public static class CreateProductVariantInputMappingExtensions
    {
        public static ProductVariant ConvertToEntity(this CreateProductVariantInput source)
        {
            return new ProductVariant
            {
                Name = source.Name,
                Values = source.Values,
                ProductId = source.ProductId
            };
        }
    }
}
