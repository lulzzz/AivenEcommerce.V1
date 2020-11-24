
using AivenEcommerce.V1.Domain.Dtos.CouponCodes;
using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Application.Mappers.CouponCodes
{
    public static class CreateCouponCodeInputMapiingExtensions
    {
        public static CouponCode ConvertToEntity(this CreateCouponCodeInput source)
        {
            return new()
            {
                Code = source.Code.ToUpper(),
                DateExpire = source.DateExpire,
                DateStart = source.DateStart,
                Categories = source.Categories,
                Customers = source.Customers,
                SubCategories = source.SubCategories,
                MinAmount = source.MinAmount,
                MaxAmount = source.MaxAmount,
                Products = source.Products,
                Value = source.Value,
                Type = source.Type
            };
        }
    }
}
