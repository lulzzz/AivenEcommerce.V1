using System;

namespace AivenEcommerce.V1.Domain.Entities
{
    public class ProductLabel
    {
        public Guid Id { get; set; }
        public Guid LabelId { get; set; }
        public Guid ProductId { get; set; }
    }
}
