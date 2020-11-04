
using AivenEcommerce.V1.Domain.Enums;

namespace AivenEcommerce.V1.Domain.Entities
{
    public class ProductDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public ProductDetailType Type { get; set; }
        public string Value { get; set; }
        public bool IsDefault { get; set; }

    }
}
