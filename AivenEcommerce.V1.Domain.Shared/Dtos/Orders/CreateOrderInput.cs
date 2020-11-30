using AivenEcommerce.V1.Domain.Shared.Dtos.Products;
using AivenEcommerce.V1.Domain.Shared.Enums;

using System;
using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Shared.Dtos.Orders
{
    public record CreateOrderInput(
        string CustomerEmail,
        string CouponCode,
        IEnumerable<ProductDefinitive> Products,
        Guid AddressId,
        PaymentProvider PaymentProvider
        );
}
