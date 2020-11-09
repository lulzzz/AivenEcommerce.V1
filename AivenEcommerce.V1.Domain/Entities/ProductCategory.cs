using System;
using System.Collections.Generic;

using AivenEcommerce.V1.Domain.Entities.Base;

namespace AivenEcommerce.V1.Domain.Entities
{
    public class ProductCategory : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> SubCategories{ get; set; }
    }
}
