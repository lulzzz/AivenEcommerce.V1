using System;
using System.Collections.Generic;

using AivenEcommerce.V1.Domain.Enums;

namespace AivenEcommerce.V1.Domain.Entities
{
    public class CouponCode
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public CouponCodeOffType Type { get; set; }
        public int Quantity { get; set; }
        public int MinAmount { get; set; }
        public int? MaxAmount { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public IEnumerable<string> SubCategories { get; set; }
        public IEnumerable<string> Products { get; set; }
        public IEnumerable<string> Users { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateExpire { get; set; }
    }
}
