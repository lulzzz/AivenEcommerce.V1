using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.Addresses;

namespace AivenEcommerce.V1.Application.Mappers.Addresses
{
    public static class CreateAddressInputMappingExtensions
    {
        public static Address ConvertToEntity(this CreateAddressInput source)
        {
            return new()
            {
                Depatarment = source.Depatarment,
                BetweenStreet1 = source.BetweenStreet1,
                BetweenStreet2 = source.BetweenStreet2,
                City = source.City,
                Name = source.Name,
                Number = source.Number,
                Observations = source.Observations,
                Phone = source.Phone,
                Street = source.Street,
                Type = source.Type,
                ZipCode = source.ZipCode,
                CustomerEmail = source.CustomerEmail
            };
        }
    }
}
