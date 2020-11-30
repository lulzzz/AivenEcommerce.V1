using AivenEcommerce.V1.Domain.Shared.Common;
using AivenEcommerce.V1.Domain.Shared.Dtos.ProductOverViews;
using AivenEcommerce.V1.Domain.Shared.OperationResults.Validations;

using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Validators
{
    public interface IProductOverviewValidator : IScopedService
    {
        Task<ValidationResult> ValidateCreateProductOverview(CreateProductOverviewInput input);
        Task<ValidationResult> ValidateUpdateProductOverview(UpdateProductOverviewInput input);
    }
}
