﻿using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.CouponCodes;

namespace AivenEcommerce.V1.Application.Mappers.CouponCodes
{
    public static class CouponCodeDtoMappingExtensions
    {
        public static CouponCodeDto ConvertToDto(this CouponCode source)
        {
            return new(source.Id, source.Code, source.Type, (int)source.Value, source.MinAmount, source.MaxAmount, source.Categories, source.SubCategories, source.Products, source.Customers, source.DateStart, source.DateExpire);
        }
    }
}
