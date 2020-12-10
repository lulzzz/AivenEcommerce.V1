using AivenEcommerce.V1.Application.Mappers.Sales;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.Sales;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace AivenEcommerce.V1.Application.Tests.Mappers.Sales
{
    public class SaleDtoMappingExtensionsShould
    {

        [Fact]
        public void ConvertToDto_SaleNotNull_ReturnSameId()
        {
            Sale sale = MockSale();

            SaleDto saleDto = sale.ConvertToDto();

            Assert.Equal(sale.Id, saleDto.Id);
        }

        [Fact]
        public void ConvertToDto_SaleNotNull_ReturnSameCouponCode()
        {
            Sale sale = MockSale();

            SaleDto saleDto = sale.ConvertToDto();

            Assert.Equal(sale.CouponCode, saleDto.CouponCode);
        }

        [Fact]
        public void ConvertToDto_SaleNotNull_ReturnSameOrderId()
        {
            Sale sale = MockSale();

            SaleDto saleDto = sale.ConvertToDto();

            Assert.Equal(sale.OrderId, saleDto.OrderId);
        }

        [Fact]
        public void ConvertToDto_SaleNotNull_ReturnSameStatus()
        {
            Sale sale = MockSale();

            SaleDto saleDto = sale.ConvertToDto();

            Assert.Equal(sale.Status, saleDto.Status);
        }

        [Fact]
        public void ConvertToDto_SaleNotNull_ReturnSameProducts()
        {
            Sale sale = MockSale();

            SaleDto saleDto = sale.ConvertToDto();

            Assert.Equal(sale.Products, saleDto.Products);
        }

        private Sale MockSale() =>
            new()
            {
                Id = nameof(Sale.Id),
                CouponCode = nameof(Sale.CouponCode),
                OrderId = nameof(Sale.OrderId),
                Status = Domain.Shared.Enums.SaleStatus.Delivered,
                Products = new[] { "f" },
            };
    }
}
