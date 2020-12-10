using AivenEcommerce.V1.Application.Mappers.Baskets;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.Baskets;
using AivenEcommerce.V1.Domain.Shared.Dtos.Products;
using AivenEcommerce.V1.Domain.Shared.Dtos.ProductVariants;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace AivenEcommerce.V1.Application.Tests.Mappers.Baskets
{
    public class BasketDtoMappingExtensionsShould
    {
        [Fact]
        public void ConvertToDto_BasketNotNull_ReturnSameCustomerEmail()
        {
            Basket basket = MockBasket();

            BasketDto basketDto = basket.ConvertToDto();

            Assert.Equal(basketDto.CustomerEmail, basket.CustomerEmail);
        }

        [Fact]
        public void ConvertToDto_BasketNotNull_ReturnSameId()
        {
            Basket basket = MockBasket();

            BasketDto basketDto = basket.ConvertToDto();

            Assert.Equal(basketDto.Id, basket.Id);
        }

        [Fact]
        public void ConvertToDto_BasketNotNull_ReturnSameProducts()
        {
            Basket basket = MockBasket();

            BasketDto basketDto = basket.ConvertToDto();

            Assert.Equal(basketDto.ProductDefinitives, basket.Products);
        }

        private Basket MockBasket() =>
        new()
        {
            CustomerEmail = nameof(Basket.CustomerEmail),
            Id = Guid.NewGuid(),
            Products = new List<ProductDefinitive>()
            {
                new (nameof(ProductDefinitive.ProductId), new List<ProductVariantPair>()
                {
                    new ProductVariantPair(nameof(ProductVariantPair.Name), nameof(ProductVariantPair.Value))
                }, 4)
            }
        };
    }
}
