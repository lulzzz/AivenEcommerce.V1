using System;
using System.Collections.Generic;

using AivenEcommerce.V1.Domain.Dtos.Products;
using AivenEcommerce.V1.Domain.Entities.Base;

namespace AivenEcommerce.V1.Domain.Entities
{
    public class Basket : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public IEnumerable<ProductDefinitive> Products { get; set; }
        public string CustomerEmail { get; set; }
    }
}
