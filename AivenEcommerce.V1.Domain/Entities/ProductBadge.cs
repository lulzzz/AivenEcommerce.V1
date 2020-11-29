using AivenEcommerce.V1.Domain.Entities.Base;
using AivenEcommerce.V1.Domain.Enums;

using System;
using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Entities
{
    public class ProductBadge : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public IEnumerable<ProductBadgeName> Badges { get; set; }
        public string ProductId { get; set; }
    }
}
