using AivenEcommerce.V1.Domain.Entities.Base;
using AivenEcommerce.V1.Domain.Shared.Dtos.Products;

using System;
using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Entities
{
    public class SaleDetail : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string SaleId { get; set; }
        public IEnumerable<ProductDefinitive> Products { get; set; }
    }
}
