using AivenEcommerce.V1.Domain.Shared.Paginations;

namespace AivenEcommerce.V1.Domain.Shared.Dtos.Products
{
    public class ProductParameters : QueryStringParameters
    {
        public bool? IsActive { get; set; }
    }
}
