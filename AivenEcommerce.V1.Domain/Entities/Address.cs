using AivenEcommerce.V1.Domain.Enums;

using System;

namespace AivenEcommerce.V1.Domain.Entities
{
    public class Address
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int? Number { get; set; }
        public string Depatarment { get; set; }
        public string BetweenStreet1 { get; set; }
        public string BetweenStreet2 { get; set; }
        public string Observations { get; set; }
        public string Phone { get; set; }
        public AddressType Type { get; set; }
        public string CustomerEmail { get; set; }
    }
}
