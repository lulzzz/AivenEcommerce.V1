using System.Threading.Tasks;

using AivenEcommerce.V1.Application.Validations;
using AivenEcommerce.V1.Domain.Common;
using AivenEcommerce.V1.Domain.Dtos.CouponCodes;

namespace AivenEcommerce.V1.Domain.Validators
{
    public interface ICouponCodeValidator : IScopedService
    {
        Task<ValidationResult> ValidateCreateCouponCodeAsync(CreateCouponCodeInput input);
        Task<ValidationResult> ValidateUpdateCouponCodeAsync(UpdateCouponCodeInput input);
        Task<ValidationResult> ValidateRemoveCouponCodeAsync(RemoveCouponCodeInput input);
    }
}
