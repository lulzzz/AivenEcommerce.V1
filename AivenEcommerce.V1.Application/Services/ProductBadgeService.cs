using AivenEcommerce.V1.Application.Mappers.ProductBadges;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Services;
using AivenEcommerce.V1.Domain.Shared.Dtos.ProductBadges;
using AivenEcommerce.V1.Domain.Shared.OperationResults;

using System;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Application.Services
{
    public class ProductBadgeService : IProductBadgeService
    {
        private readonly IProductBadgeRepository _productBadgeRepository;

        public ProductBadgeService(IProductBadgeRepository productBadgeRepository)
        {
            _productBadgeRepository = productBadgeRepository ?? throw new ArgumentNullException(nameof(productBadgeRepository));
        }

        public async Task<OperationResult<ProductBadgeDto>> GetByProductAsync(string productId)
        {
            ProductBadge badge = await _productBadgeRepository.GetByProduct(new Product
            {
                Id = productId
            });

            if (badge is null)
            {
                return OperationResult<ProductBadgeDto>.NotFound();
            }

            return OperationResult<ProductBadgeDto>.Success(badge.ConvertToDto());
        }
    }
}
