using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.Sales;

namespace AivenEcommerce.V1.Application.Mappers.Sales
{
    public static class SaleDtoMappingExtensions
    {
        public static SaleDto ConvertToDto(this Sale source)
        {
            return new(source.Id, source.Products, source.CouponCode, source.OrderId);
        }
    }
}
