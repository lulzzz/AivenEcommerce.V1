using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.Addresses;

namespace AivenEcommerce.V1.Application.Mappers.Addresses
{
    public static class UpdateAddressInputMappingExtensions
    {
        public static Address ConvertToEntity(this UpdateAddressInput source)
        {
            return new()
            {
                Id = source.AddressId,
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
