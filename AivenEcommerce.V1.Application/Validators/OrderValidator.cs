using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Shared.Dtos.Orders;
using AivenEcommerce.V1.Domain.Shared.Enums;
using AivenEcommerce.V1.Domain.Shared.OperationResults.Validations;
using AivenEcommerce.V1.Domain.Validators;

using System;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Application.Validators
{
    public class OrderValidator : IOrderValidator
    {
        private readonly IOrderRepository _orderRepository;

        public OrderValidator(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<ValidationResult> ValidateCancelOrderAsync(CancelOrderInput input)
        {
            ValidationResult validationResult = new();

            Order order = await _orderRepository.GetAsync(input.Id);

            if (order is null)
            {
                validationResult.Messages.Add(new(nameof(CancelOrderInput.Id), "La orden de pago no existe."));
            }
            else
            {
                switch (order.Status)
                {
                    case OrderStatus.Payed:
                        validationResult.Messages.Add(new(nameof(CancelOrderInput.Id), "No se puede cancelar una orden que ya fue pagada."));
                        break;
                    case OrderStatus.Canceled:
                        validationResult.Messages.Add(new(nameof(CancelOrderInput.Id), "No se puede cancelar una orden que ya fue cancelada."));
                        break;
                }

            }

            return validationResult;
        }
    }
}
