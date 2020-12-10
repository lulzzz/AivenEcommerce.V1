using AivenEcommerce.V1.Application.Mappers.Orders;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.Orders;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace AivenEcommerce.V1.Application.Tests.Mappers.Orders
{
    public class OrderDtoMappingExtensionsShould
    {
        [Fact]
        public void ConvertToDto_OrderNotNull_ReturnSameId()
        {
            Order order = MockOrder();

            OrderDto orderDto = order.ConvertToDto();

            Assert.Equal(order.Id, orderDto.Id);
        }

        [Fact]
        public void ConvertToDto_OrderNotNull_ReturnSameTotalAmount()
        {
            Order order = MockOrder();

            OrderDto orderDto = order.ConvertToDto();

            Assert.Equal(order.TotalAmount, orderDto.TotalAmount);
        }

        [Fact]
        public void ConvertToDto_OrderNotNull_ReturnSameCurrency()
        {
            Order order = MockOrder();

            OrderDto orderDto = order.ConvertToDto();

            Assert.Equal(order.Currency, orderDto.Currency);
        }

        [Fact]
        public void ConvertToDto_OrderNotNull_ReturnSameStatus()
        {
            Order order = MockOrder();

            OrderDto orderDto = order.ConvertToDto();

            Assert.Equal(order.Status, orderDto.Status);
        }

        [Fact]
        public void ConvertToDto_OrderNotNull_ReturnSameCreationDate()
        {
            Order order = MockOrder();

            OrderDto orderDto = order.ConvertToDto();

            Assert.Equal(order.CreationDate, orderDto.CreationDate);
        }

        [Fact]
        public void ConvertToDto_OrderNotNull_ReturnSamePayDate()
        {
            Order order = MockOrder();

            OrderDto orderDto = order.ConvertToDto();

            Assert.Equal(order.PayDate, orderDto.PayDate);
        }

        [Fact]
        public void ConvertToDto_OrderNotNull_ReturnSameType()
        {
            Order order = MockOrder();

            OrderDto orderDto = order.ConvertToDto();

            Assert.Equal(order.Type, orderDto.Type);
        }

        private Order MockOrder() =>
        new()
        {
            Id = nameof(Order.Id),
            TotalAmount = 1000,
            Currency = Domain.Shared.Enums.Currency.USD,
            Status = Domain.Shared.Enums.OrderStatus.Canceled,
            CreationDate = DateTime.Now,
            PayDate = DateTime.Now,
            Type = Domain.Shared.Enums.OrderType.Other
        };
    }
}
