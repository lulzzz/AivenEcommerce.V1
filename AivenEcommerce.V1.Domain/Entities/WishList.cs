using AivenEcommerce.V1.Domain.Entities.Base;

using System;
using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Entities
{
    public class WishList : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string CustomerEmail { get; set; }
        public IEnumerable<string> Products { get; set; }
    }
}
