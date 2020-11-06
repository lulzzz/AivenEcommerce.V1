using System;

using AivenEcommerce.V1.Domain.Entities.Base;
using AivenEcommerce.V1.Domain.Enums;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AivenEcommerce.V1.Domain.Entities
{
    public class Product : IEntity<string>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public short PercentageOff { get; set; }
        public ProductCategory Category { get; set; }
        public ProductSubCategory SubCategory { get; set; }
        public int Stock { get; set; }
        public Uri Thumbnail { get; set; }
        public bool IsActive { get; set; }

    }
}
