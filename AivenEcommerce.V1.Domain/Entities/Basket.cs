using AivenEcommerce.V1.Domain.Entities.Base;
using AivenEcommerce.V1.Domain.Shared.Dtos.Products;

using System;
using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Entities
{
    public class Basket : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public IEnumerable<ProductDefinitive> Products { get; set; }
        public string CustomerEmail { get; set; }
    }
}
