﻿using System;

namespace AivenEcommerce.V1.Domain.Dtos.Addresses
{
    public record DeleteAddressInput
    (
        Guid Id,
        string CustomerEmail
    );
}
