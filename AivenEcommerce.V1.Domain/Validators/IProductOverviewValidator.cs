using System.Threading.Tasks;

using AivenEcommerce.V1.Application.Validations;
using AivenEcommerce.V1.Domain.Common;
using AivenEcommerce.V1.Domain.Dtos.ProductOverViews;

namespace AivenEcommerce.V1.Domain.Validators
{
    public interface IProductOverviewValidator : IScopedService
    {
        Task<ValidationResult> ValidateCreateProductOverview(CreateProductOverviewInput input);
        Task<ValidationResult> ValidateUpdateProductOverview(UpdateProductOverviewInput input);
    }
}
