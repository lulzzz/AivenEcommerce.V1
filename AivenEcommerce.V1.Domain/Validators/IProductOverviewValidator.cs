using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AivenEcommerce.V1.Application.Validations;
using AivenEcommerce.V1.Domain.Dtos.ProductOverViews;

namespace AivenEcommerce.V1.Domain.Validators
{
    public interface IProductOverviewValidator
    {
        Task<ValidationResult> ValidateCreateProductOverview(CreateProductOverviewInput input);
        Task<ValidationResult> ValidateUpdateProductOverview(UpdateProductOverviewInput input);
    }
}
