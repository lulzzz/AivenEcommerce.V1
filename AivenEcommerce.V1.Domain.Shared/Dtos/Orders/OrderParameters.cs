using AivenEcommerce.V1.Domain.Shared.Enums;
using AivenEcommerce.V1.Domain.Shared.Paginations;

namespace AivenEcommerce.V1.Domain.Shared.Dtos.Orders
{
    public class OrderParameters : QueryStringParameters
    {
        public OrderStatus? Status { get; set; }
    }
}
