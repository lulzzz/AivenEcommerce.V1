using AivenEcommerce.V1.Application.Mappers.Orders;
using AivenEcommerce.V1.Application.Mappers.Paginations;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.Orders;
using AivenEcommerce.V1.Domain.Shared.Paginations;

using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace AivenEcommerce.V1.Application.Tests.Mappers.Paginations
{
    public class PagedDataMappingExtensionsShould
    {
        [Fact]
        public void ConvertToDto_PagedDataNotNull_ReturnItemsDto()
        {
            PagedData<Order> pagedDate = MockPagedData();
            Func<Order, OrderDto> selector = x => x.ConvertToDto();

            PagedData<OrderDto> pagedDateDto = pagedDate.ConvertToDto(selector);

            Assert.Equal(pagedDate.Items.Select(selector), pagedDateDto.Items);
        }

        [Fact]
        public void ConvertToDto_PagedDataNotNull_ReturnTotalCount()
        {
            PagedData<Order> pagedDate = MockPagedData();
            Func<Order, OrderDto> selector = x => x.ConvertToDto();

            PagedData<OrderDto> pagedDateDto = pagedDate.ConvertToDto(selector);

            Assert.Equal(pagedDate.TotalCount, pagedDateDto.TotalCount);
        }

        private PagedData<Order> MockPagedData() =>
            new()
            {
                Items = new List<Order>(){
                    new()
                    {
                        Id = nameof(Order.Id),
                        TotalAmount = 1000,
                        Currency = Domain.Shared.Enums.Currency.USD,
                        Status = Domain.Shared.Enums.OrderStatus.Canceled,
                        CreationDate = DateTime.Now,
                        PayDate = DateTime.Now,
                        Type = Domain.Shared.Enums.OrderType.Other
                    }
                },
                TotalCount = 10
            };
    }
}
