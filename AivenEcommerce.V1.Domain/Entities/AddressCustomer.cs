using AivenEcommerce.V1.Domain.Entities.Base;

using System;
using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Entities
{
    public class AddressCustomer : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public IEnumerable<Address> Addresses { get; set; }
        public string CustomerEmail { get; set; }
    }
}
