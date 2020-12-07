using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.Orders;

namespace AivenEcommerce.V1.Application.Mappers.Orders
{
    public static class OrderDtoMappingExtensions
    {
        public static OrderDto ConvertToDto(this Order source)
        {
            return new(source.Id, source.CustomerEmail, source.TotalAmount, source.Status, source.Type, source.Currency, source.CreationDate, source.PayDate);
        }
    }
}
