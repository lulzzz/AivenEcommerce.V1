
using System;

namespace AivenEcommerce.V1.Domain.Shared.Dtos.Addresses
{
    public record DeleteAddressInput
    (
        Guid AddressId,
        string CustomerEmail
    );
}
