
using AivenEcommerce.V1.Domain.Entities.Base;
using AivenEcommerce.V1.Domain.Enums;

using System;

namespace AivenEcommerce.V1.Domain.Entities
{
    public class Delivery : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string OrderId { get; set; }
        public Guid AddressId { get; set; }
        public DeliveryStatus Status { get; set; }
    }
}
