using System;
using System.Collections.Generic;

using AivenEcommerce.V1.Domain.Dtos.ProductCategories;
using AivenEcommerce.V1.Domain.Enums;

namespace AivenEcommerce.V1.Domain.Dtos.CouponCodes
{
    public record UpdateCouponCodeInput(
        Guid Id,
        string Code,
        CouponCodeOffType Type,
        decimal Value,
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
