using System;
using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Entities
{
    public class GiftCard
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public short Percentage { get; set; }
        public IEnumerable<string> Products { get; set; }
    }
}
