﻿using AivenEcommerce.V1.Domain.Common;
using AivenEcommerce.V1.Domain.Dtos.ProductBadges;
using AivenEcommerce.V1.Domain.OperationResults;

using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Services
{
    public interface IProductBadgeService : IScopedService
    {
        Task<OperationResult<ProductBadgeDto>> GetByProductAsync(string productId);
    }
}
