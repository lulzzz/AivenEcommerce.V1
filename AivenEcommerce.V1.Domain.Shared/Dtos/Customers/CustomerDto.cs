
using System;

namespace AivenEcommerce.V1.Domain.Shared.Dtos.Customers
{
    public record CustomerDto(Guid Id, string Name, string LastName, string Email, Uri Picture);
}
