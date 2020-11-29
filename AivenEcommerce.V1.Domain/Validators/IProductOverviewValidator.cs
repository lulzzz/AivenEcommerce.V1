using AivenEcommerce.V1.Application.Validations;
using AivenEcommerce.V1.Domain.Common;
using AivenEcommerce.V1.Domain.Dtos.ProductOverViews;

using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Validators
{
    public interface IProductOverviewValidator : IScopedService
    {
        Task<ValidationResult> ValidateCreateProductOverview(CreateProductOverviewInput input);
        Task<ValidationResult> ValidateUpdateProductOverview(UpdateProductOverviewInput input);
    }
}
