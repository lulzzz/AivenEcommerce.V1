using AivenEcommerce.V1.Domain.Entities.Base;

using System;

namespace AivenEcommerce.V1.Domain.Entities
{
    public class ProductImage : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string ProductId { get; set; }
        public Uri Image { get; set; }
    }
}
