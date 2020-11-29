using AivenEcommerce.V1.Domain.Entities.Base;

using System;

namespace AivenEcommerce.V1.Domain.Entities
{
    public class ProductOverview : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string ProductId { get; set; }
        public string Description { get; set; }
    }
}
