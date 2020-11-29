using AivenEcommerce.V1.Domain.Entities.Base;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Entities
{
    public class Sale : IEntity<string>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public IEnumerable<string> Products { get; set; }
        public string CouponCode { get; set; }
        public string OrderId { get; set; }

    }
}
