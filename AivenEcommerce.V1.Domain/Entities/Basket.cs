
using System;

using AivenEcommerce.V1.Domain.Entities.Base;
using AivenEcommerce.V1.Domain.Enums;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AivenEcommerce.V1.Domain.Entities
{
    public class Basket : IEntity<string>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Guid UserId { get; set; }
        public BasketStatus Status { get; set; }
    }
}
