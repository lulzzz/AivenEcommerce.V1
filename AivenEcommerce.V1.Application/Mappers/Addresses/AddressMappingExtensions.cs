using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.Addresses;

namespace AivenEcommerce.V1.Application.Mappers.Addresses
{
    public static class AddressMappingExtensions
    {
        public static AddressDto ConvertToDto(this Address source)
        {
            return new(source.Id, source.Name, source.ZipCode, source.City,
                source.Street, source.Number, source.Depatarment, source.BetweenStreet1, source.BetweenStreet2, source.Observations,
                source.Phone, source.Type, source.CustomerEmail);
        }
    }
}
