using AivenEcommerce.V1.Domain.Entities.Base;

using System;
using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Entities
{
    public class ProductCategory : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> SubCategories { get; set; }
    }
}
