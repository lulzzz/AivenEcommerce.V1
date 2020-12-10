using AivenEcommerce.V1.Application.Mappers.Products;
using AivenEcommerce.V1.Application.Mappers.WishLists;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.WishLists;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace AivenEcommerce.V1.Application.Tests.Mappers.WishLists
{
    public class WishListProductsDtoMappingExtensionsShould
    {
        [Fact]
        public void ConvertToDto_WishListNotNull_ReturnSameId()
        {
            WishList wishlist = MockWishList();

            WishListProductsDto wishlistDto = wishlist.ConvertToDto(MockIEnumerableProduct());

            Assert.Equal(wishlist.Id, wishlistDto.Id);
        }

        [Fact]
        public void ConvertToDto_WishListNotNull_ReturnSameCustomerEmail()
        {
            WishList wishlist = MockWishList();

            WishListProductsDto wishlistDto = wishlist.ConvertToDto(MockIEnumerableProduct());

            Assert.Equal(wishlist.CustomerEmail, wishlistDto.CustomerEmail);
        }


        [Fact]
        public void ConvertToDto_WishListNotNull_ReturnProductsDto()
        {
            WishList wishlist = MockWishList();

            WishListProductsDto wishlistDto = wishlist.ConvertToDto(MockIEnumerableProduct());

            Assert.Equal(MockIEnumerableProduct().Select(x => x.ConvertToDto()), wishlistDto.Products);
        }

        private WishList MockWishList() =>
            new()
            {
                Id = Guid.NewGuid(),
                CustomerEmail = nameof(WishList.CustomerEmail),
                Products = new[] { "a" }
            };

        private IEnumerable<Product> MockIEnumerableProduct() =>
            new List<Product>
            {
                new()
                {
                    Id = "1",
                    Category = "Category",
                    Cost = 1,
                    IsActive = true,
                    Name = "Name",
                    PercentageOff = 1,
                    Price = 2,
                    Stock = 3,
                    SubCategory = "SubCategory",
                    Thumbnail = new("http://contoso.com")
                }
            };
    }
}
