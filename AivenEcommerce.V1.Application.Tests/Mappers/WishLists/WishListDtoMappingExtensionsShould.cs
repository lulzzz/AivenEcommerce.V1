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
    public class WishListDtoMappingExtensionsShould
    {

        [Fact]
        public void ConvertToDto_WishListNotNull_ReturnSameId()
        {
            WishList sale = MockWishList();

            WishListDto saleDto = sale.ConvertToDto();

            Assert.Equal(sale.Id, saleDto.Id);
        }

        [Fact]
        public void ConvertToDto_WishListNotNull_ReturnSameCustomerEmail()
        {
            WishList sale = MockWishList();

            WishListDto saleDto = sale.ConvertToDto();

            Assert.Equal(sale.CustomerEmail, saleDto.CustomerEmail);
        }

        [Fact]
        public void ConvertToDto_WishListNotNull_ReturnSameProducts()
        {
            WishList sale = MockWishList();

            WishListDto saleDto = sale.ConvertToDto();

            Assert.Equal(sale.Products, saleDto.Products);
        }

        private WishList MockWishList() =>
            new()
            {
                Id = Guid.NewGuid(),
                CustomerEmail = nameof(WishList.CustomerEmail),
                Products = new[] { "f" },
            };
    }
}
