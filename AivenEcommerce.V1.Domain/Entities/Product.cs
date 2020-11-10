using System;

using AivenEcommerce.V1.Domain.Entities.Base;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AivenEcommerce.V1.Domain.Entities
{
    public class Product : IEntity<string>
    {
        public Product()
        {
            Name = string.Empty;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public short PercentageOff { get; set; }
        public string? Category { get; set; }
        public string? SubCategory { get; set; }
        public int Stock { get; set; }
        public Uri? Thumbnail { get; set; }
        public bool IsActive { get; set; }

    }
}
