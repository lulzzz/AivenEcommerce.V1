using AivenEcommerce.V1.Application.Extensions;
using AivenEcommerce.V1.Application.Mappers.Orders;
using AivenEcommerce.V1.Application.Mappers.Paginations;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Services;
using AivenEcommerce.V1.Domain.Shared.Dtos.Orders;
using AivenEcommerce.V1.Domain.Shared.OperationResults;
using AivenEcommerce.V1.Domain.Shared.Paginations;
using AivenEcommerce.V1.Domain.Validators;

using System;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderValidator _orderValidator;

        public OrderService(IOrderRepository orderRepository, IOrderValidator orderValidator)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _orderValidator = orderValidator ?? throw new ArgumentNullException(nameof(orderValidator));
        }

        public async Task<OperationResult<OrderDto>> CancelOrderAsync(CancelOrderInput input)
        {
            var validationResult = await _orderValidator.ValidateCancelOrderAsync(input);

            if (validationResult.IsSuccess)
            {
                var order = await _orderRepository.GetAsync(input.Id);
                order.Status = Domain.Shared.Enums.OrderStatus.Canceled;

                await _orderRepository.UpdateAsync(order);

                return OperationResult<OrderDto>.Success(order.ConvertToDto());
            }

            return OperationResult<OrderDto>.Fail(validationResult);
        }

        public async Task<OperationResult<PagedResult<OrderDto>>> GetAllAsync(OrderParameters parameters)
        {
            PagedData<Order> pagedData = await _orderRepository.GetAllAsync(parameters);

            PagedData<OrderDto> pagedDataDto = pagedData.ConvertToDto(x => x.ConvertToDto());

            return OperationResult<PagedResult<OrderDto>>.Success(pagedDataDto.ConvertToPagedResult(parameters));
        }
    }
}
