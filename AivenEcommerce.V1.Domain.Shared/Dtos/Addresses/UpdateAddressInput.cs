using AivenEcommerce.V1.Domain.Shared.Enums;

using System;

namespace AivenEcommerce.V1.Domain.Shared.Dtos.Addresses
{
    public record UpdateAddressInput
    (
        Guid Id,
        string Name,
        string ZipCode,
        string City,
        string Street,
        int? Number,
        string Depatarment,
        string BetweenStreet1,
        string BetweenStreet2,
        string Observations,
        string Phone,
        AddressType Type,
        string CustomerEmail
    );
}
