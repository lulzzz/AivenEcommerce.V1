using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Shared.Dtos.Sales
{
    public record SaleDto(string Id, IEnumerable<string> Products, string CouponCode, string OrderId);
}
