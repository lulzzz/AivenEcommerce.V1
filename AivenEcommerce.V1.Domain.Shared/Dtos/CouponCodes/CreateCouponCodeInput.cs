﻿using AivenEcommerce.V1.Domain.Shared.Dtos.ProductCategories;
using AivenEcommerce.V1.Domain.Shared.Enums;

using System;
using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Shared.Dtos.CouponCodes
{
    public record CreateCouponCodeInput(
        string Code,
        CouponCodeOffType Type,
        int Value,
        int MinAmount,
        int? MaxAmount,
        IEnumerable<string> Categories,
        IEnumerable<ProductCategoryPair> SubCategories,
        IEnumerable<string> Products,
        IEnumerable<string> Customers,
        DateTime DateStart,
        DateTime? DateExpire
        );
}
