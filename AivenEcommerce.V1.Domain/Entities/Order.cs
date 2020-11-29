using AivenEcommerce.V1.Domain.Entities.Base;
using AivenEcommerce.V1.Domain.Enums;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using System;

namespace AivenEcommerce.V1.Domain.Entities
{
    public class Order : IEntity<string>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string CustomerEmail { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public OrderType Type { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime PayDate { get; set; }
    }
}
