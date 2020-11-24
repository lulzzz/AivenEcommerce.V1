using System;
using System.Linq;
using System.Threading.Tasks;

using AivenEcommerce.V1.Application.Mappers.CouponCodes;
using AivenEcommerce.V1.Domain.Dtos.CouponCodes;
using AivenEcommerce.V1.Domain.OperationResults;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Services;
using AivenEcommerce.V1.Domain.Validators;

namespace AivenEcommerce.V1.Application.Services
{
    public class CouponCodeService : ICouponCodeService
    {
        private readonly ICouponCodeRepository _couponCodeRepository;
        private readonly ICouponCodeValidator _couponCodeValidator;

        public CouponCodeService(ICouponCodeRepository couponCodeRepository, ICouponCodeValidator couponCodeValidator)
        {
            _couponCodeRepository = couponCodeRepository ?? throw new ArgumentNullException(nameof(couponCodeRepository));
            _couponCodeValidator = couponCodeValidator ?? throw new ArgumentNullException(nameof(couponCodeValidator));
        }

        public async Task<OperationResult<CouponCodeDto>> CreateCouponCodeAsync(CreateCouponCodeInput input)
        {
            var validationResult = await _couponCodeValidator.ValidateCreateCouponCodeAsync(input);
            if (validationResult.IsSuccess)
            {
                var entity = input.ConvertToEntity();

                entity = await _couponCodeRepository.CreateAsync(entity);

                return OperationResult<CouponCodeDto>.Success(entity.ConvertToDto());
            }

            return OperationResult<CouponCodeDto>.Fail(validationResult);
        }

        public async Task<OperationResultEnumerable<CouponCodeDto>> GetCouponCodeAsync()
        {
            var couponCodes = await _couponCodeRepository.GetAllAsync();

            return OperationResultEnumerable<CouponCodeDto>.Success(couponCodes.Select(x => x.ConvertToDto()));
        }

        public async Task<OperationResult<CouponCodeDto>> GetCouponCodeAsync(string code)
        {
            var couponCode = await _couponCodeRepository.GetCouponAsync(code.ToUpper());

            if (couponCode is null)
            {
                return OperationResult<CouponCodeDto>.NotFound();
            }

            return OperationResult<CouponCodeDto>.Success(couponCode.ConvertToDto());
        }

        public async Task<OperationResult> RemoveCouponCodeAsync(RemoveCouponCodeInput input)
        {
            var validationResult = await _couponCodeValidator.ValidateRemoveCouponCodeAsync(input);
            if (validationResult.IsSuccess)
            {
                var entity = await _couponCodeRepository.GetCouponAsync(input.Code.ToUpper());
                await _couponCodeRepository.RemoveAsync(entity);

                return OperationResult.Success();
            }

            return OperationResult<CouponCodeDto>.Fail(validationResult);
        }

        public async Task<OperationResult<CouponCodeDto>> UpdateCouponCodeAsync(UpdateCouponCodeInput input)
        {
            var validationResult = await _couponCodeValidator.ValidateUpdateCouponCodeAsync(input);
            if (validationResult.IsSuccess)
            {
                var entity = await _couponCodeRepository.GetCouponAsync(input.Code.ToUpper());
                entity.Categories = input.Categories;
                entity.Customers = input.Customers;
                entity.DateExpire = input.DateExpire;
                entity.DateStart = input.DateStart;
                entity.MaxAmount = input.MaxAmount;
                entity.MinAmount = input.MinAmount;
                entity.Products = input.Products;
                entity.Customers = input.Customers;
                entity.Value = input.Value;
                entity.SubCategories = input.SubCategories;
                entity.Type = input.Type;

                await _couponCodeRepository.UpdateAsync(entity);

                return OperationResult<CouponCodeDto>.Success(entity.ConvertToDto());
            }

            return OperationResult<CouponCodeDto>.Fail(validationResult);
        }
    }
}
