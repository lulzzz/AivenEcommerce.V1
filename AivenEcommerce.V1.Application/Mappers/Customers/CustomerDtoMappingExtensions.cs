using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.Customers;

namespace AivenEcommerce.V1.Application.Mappers.Customers
{
    public static class CustomerDtoMappingExtensions
    {
        public static CustomerDto ConvertToDto(this Customer source)
        {
            return new(source.Id, source.Name, source.LastName, source.Email, source.Picture);
        }
    }
}
