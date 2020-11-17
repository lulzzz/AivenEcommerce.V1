using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AivenEcommerce.V1.Application.Validations;
using AivenEcommerce.V1.Domain.Dtos.ProductVariants;

namespace AivenEcommerce.V1.Domain.Validators
{
    public interface IProductVariantValidator
    {
        Task<ValidationResult> ValidateCreateProductVariant(CreateProductVariantInput input);
        Task<ValidationResult> ValidateUpdateProductVariant(UpdateProductVariantInput input);
        Task<ValidationResult> ValidateDeleteProductVariant(DeleteProductVariantInput input);
    }
}
