using System;

using AivenEcommerce.V1.Domain.Enums;

namespace AivenEcommerce.V1.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int BasketId { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public Uri Link { get; set; }
        public string Transaction { get; set; }
    }
}
