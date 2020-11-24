using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Common;
using AivenEcommerce.V1.Domain.Dtos.CouponCodes;
using AivenEcommerce.V1.Domain.OperationResults;

namespace AivenEcommerce.V1.Domain.Services
{
    public interface ICouponCodeService : IScopedService
    {
        Task<OperationResultEnumerable<CouponCodeDto>> GetCouponCodeAsync();
        Task<OperationResult<CouponCodeDto>> GetCouponCodeAsync(string code);
        Task<OperationResult<CouponCodeDto>> CreateCouponCodeAsync(CreateCouponCodeInput input);
        Task<OperationResult<CouponCodeDto>> UpdateCouponCodeAsync(UpdateCouponCodeInput input);
        Task<OperationResult> RemoveCouponCodeAsync(RemoveCouponCodeInput input);

    }
}
