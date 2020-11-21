
using AivenEcommerce.V1.Domain.Dtos.Products;

namespace AivenEcommerce.V1.Domain.Dtos.Baskets
{
    public record AddBasketProductInput(ProductDefinitive Product, string CustomerEmail);
}
