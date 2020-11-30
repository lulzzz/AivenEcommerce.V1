using AivenEcommerce.V1.Domain.Shared.Dtos.Products;

namespace AivenEcommerce.V1.Domain.Shared.Dtos.Baskets
{
    public record AddBasketProductInput(ProductDefinitive Product, string CustomerEmail);
}
