using System;
using System.Collections.Generic;

using AivenEcommerce.V1.Domain.Dtos.ProductCategories;
using AivenEcommerce.V1.Domain.Entities.Base;
using AivenEcommerce.V1.Domain.Enums;

namespace AivenEcommerce.V1.Domain.Entities
{
    public class CouponCode : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public CouponCodeOffType Type { get; set; }
        public decimal Value { get; set; }
        public int MinAmount { get; set; }
        public int? MaxAmount { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public IEnumerable<ProductCategoryPair> SubCategories { get; set; }
        public IEnumerable<string> Products { get; set; }
        public IEnumerable<string> Customers { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateExpire { get; set; }
    }
}
