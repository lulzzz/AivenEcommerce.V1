
using System;
using System.Collections.Generic;

using AivenEcommerce.V1.Domain.Entities.Base;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AivenEcommerce.V1.Domain.Entities
{
    public class ProductVariant : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string ProductId { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Values { get; set; }
    }
}
