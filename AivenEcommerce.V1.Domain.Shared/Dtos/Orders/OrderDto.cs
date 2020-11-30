using AivenEcommerce.V1.Domain.Shared.Enums;

using System;
using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Shared.Dtos.Orders
{
    public record OrderDto(
        string Id,
        IEnumerable<string> Products,
        string CustomerEmail,
        string CouponCode,
        decimal TotalAmount,
        OrderStatus Status,
        DateTime CreationDate);
}
