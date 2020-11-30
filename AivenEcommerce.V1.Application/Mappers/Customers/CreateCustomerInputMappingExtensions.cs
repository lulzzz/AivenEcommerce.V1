using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.Customers;

namespace AivenEcommerce.V1.Application.Mappers.Customers
{
    public static class CreateCustomerInputMappingExtensions
    {
        public static Customer ConvertToEntity(this CreateCustomerInput source)
        {
            return new()
            {
                Name = source.Name,
                LastName = source.LastName,
                Email = source.Email,
                Picture = source.Picture
            };
        }
    }
}
