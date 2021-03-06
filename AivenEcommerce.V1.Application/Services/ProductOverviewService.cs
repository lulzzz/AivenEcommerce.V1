﻿using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Services;
using AivenEcommerce.V1.Domain.Shared.Dtos.ProductOverViews;
using AivenEcommerce.V1.Domain.Shared.OperationResults;

using System;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Application.Services
{
    public class ProductOverviewService : IProductOverviewService
    {
        private readonly IProductOverviewRepository _overviewRepository;

        public ProductOverviewService(IProductOverviewRepository overviewRepository)
        {
            _overviewRepository = overviewRepository ?? throw new ArgumentNullException(nameof(overviewRepository));
        }

        public async Task<OperationResult<ProductOverviewDto>> GetByProductAsync(string productId)
        {
            var productOverview = await _overviewRepository.GetByProduct(new()
            {
                Id = productId
            });

            return OperationResult<ProductOverviewDto>.Success(new(productOverview.Id, productOverview.ProductId, productOverview.Description));
        }

    }
}
