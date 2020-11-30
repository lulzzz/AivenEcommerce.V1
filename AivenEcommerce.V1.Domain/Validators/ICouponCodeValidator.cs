using AivenEcommerce.V1.Domain.Shared.Common;
using AivenEcommerce.V1.Domain.Shared.Dtos.CouponCodes;
using AivenEcommerce.V1.Domain.Shared.OperationResults.Validations;

using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Validators
{
    public interface ICouponCodeValidator : IScopedService
    {
        Task<ValidationResult> ValidateCreateCouponCodeAsync(CreateCouponCodeInput input);
        Task<ValidationResult> ValidateUpdateCouponCodeAsync(UpdateCouponCodeInput input);
        Task<ValidationResult> ValidateRemoveCouponCodeAsync(RemoveCouponCodeInput input);
    }
}
