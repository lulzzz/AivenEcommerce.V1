using AivenEcommerce.V1.Domain.Dtos.Products;
using AivenEcommerce.V1.Domain.Enums;

using System;
using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Dtos.Orders
{
    public record CreateOrderInput(
        string CustomerEmail,
        string CouponCode,
        IEnumerable<ProductDefinitive> Products,
        Guid AddressId,
        PaymentProvider PaymentProvider
        );
}
