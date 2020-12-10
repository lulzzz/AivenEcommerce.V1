using AivenEcommerce.V1.Application.Mappers.Baskets;
using AivenEcommerce.V1.Application.Mappers.Products;
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
    public class BasketProductsDtoMappingExtenionsShould
    {

        [Fact]
        public void ConvertToDto_ProductNotNull_ReturnSameCustomerEmail()
        {
            Basket basket = MockBasket();
            IEnumerable<Product> products = MockIEnumerableProducts();

            BasketProductsDto basketProductsDto = basket.ConvertToDto(products);

            Assert.Equal(basket.CustomerEmail, basketProductsDto.CustomerEmail);
        }

        [Fact]
        public void ConvertToDto_ProductNotNull_ReturnSameId()
        {
            Basket basket = MockBasket();
            IEnumerable<Product> products = MockIEnumerableProducts();

            BasketProductsDto basketProductsDto = basket.ConvertToDto(products);

            Assert.Equal(basket.Id, basketProductsDto.Id);
        }

        [Fact]
        public void ConvertToDto_ProductNotNull_ReturnSameProductDefinitives()
        {
            Basket basket = MockBasket();
            IEnumerable<Product> products = MockIEnumerableProducts();

            BasketProductsDto basketProductsDto = basket.ConvertToDto(products);

            Assert.Equal(basket.Products, basketProductsDto.ProductDefinitives);
        }

        [Fact]
        public void ConvertToDto_ProductNotNull_ReturnSameProducts()
        {
            Basket basket = MockBasket();
            IEnumerable<Product> products = MockIEnumerableProducts();

            BasketProductsDto basketProductsDto = basket.ConvertToDto(products);

            Assert.Equal(products.Select(x => x.ConvertToDto()), basketProductsDto.Products);
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

        private IEnumerable<Product> MockIEnumerableProducts() =>

             new List<Product>()
             {
                 new Product()
                 {
                     Id =nameof(ProductDto.Id),
                     Name = nameof(ProductDto.Name),
                     Cost = 1,
                     Price = 2,
                     Stock = 3,
                     Category = nameof(ProductDto.Category),
                     SubCategory = nameof(ProductDto.SubCategory),
                     PercentageOff = 4,
                     Thumbnail = new("http://contoso.com"),
                     IsActive = true

                 }

             };
    }
}
