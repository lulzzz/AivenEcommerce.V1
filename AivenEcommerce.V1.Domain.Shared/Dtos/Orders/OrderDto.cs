using AivenEcommerce.V1.Domain.Shared.Enums;

using System;

namespace AivenEcommerce.V1.Domain.Shared.Dtos.Orders
{
    public record OrderDto(
        string Id,
        string CustomerEmail,
        decimal TotalAmount,
        OrderStatus Status,
        OrderType Type,
        Currency Currency,
        DateTime CreationDate,
        DateTime? PayDate
        );
}
