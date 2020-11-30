using AivenEcommerce.V1.Domain.Entities.Base;
using AivenEcommerce.V1.Domain.Shared.Enums;

using System;

namespace AivenEcommerce.V1.Domain.Entities
{
    public class Invoice : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Uri Link { get; set; }
        public string Transaction { get; set; }
        public string OrderId { get; set; }
        public bool PaymentProviderWebhookReceived { get; set; }
        public PaymentProvider PaymentProvider { get; set; }
    }
}
