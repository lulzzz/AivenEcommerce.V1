
using AivenEcommerce.V1.Domain.Entities.Base;
using AivenEcommerce.V1.Domain.Enums;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AivenEcommerce.V1.Domain.Entities
{
    public class Delivery : IEntity<string>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int Code { get; set; }
        public DeliveryStatus Status { get; set; }
        public string Observations { get; set; }
    }
}
