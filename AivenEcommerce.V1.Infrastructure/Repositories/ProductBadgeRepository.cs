using System;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Infrastructure.Extensions;
using AivenEcommerce.V1.Infrastructure.Repositories.Base;
using AivenEcommerce.V1.Modules.GitHub.Options;
using AivenEcommerce.V1.Modules.GitHub.Services;

namespace AivenEcommerce.V1.Infrastructure.Repositories
{
    public class ProductBadgeRepository : GitHubRepository<ProductBadge, Guid>, IProductBadgeRepository
    {
        private readonly IGitHubOptions _gitHubOptions;
        public ProductBadgeRepository(IGitHubService githubService, IGitHubOptions gitHubOptions) : base(githubService)
        {
            _gitHubOptions = gitHubOptions;
        }

        public async Task<ProductBadge> GetByProduct(Product product)
        {
            var file = await base._githubService.GetFileContentAsync(_gitHubOptions.ProductBadgeRepositoryId, "products", product.Id);

            return file?.Content?.Deserialize<ProductBadge>();
        }

        public override async Task<ProductBadge> CreateAsync(ProductBadge entity)
        {
            entity.Id = Guid.NewGuid();
            await base._githubService.CreateFileAsync(_gitHubOptions.ProductBadgeRepositoryId, "products", entity.ProductId, entity.Serialize());

            return entity;
        }

        public override Task UpdateAsync(ProductBadge entityIn)
        {
            return base._githubService.UpdateFileAsync(_gitHubOptions.ProductBadgeRepositoryId, "products", entityIn.ProductId, entityIn.Serialize());
        }

        public override Task RemoveAsync(ProductBadge entityIn)
        {
            return base._githubService.DeleteFileAsync(_gitHubOptions.ProductBadgeRepositoryId, "products", entityIn.ProductId);
        }
    }
}
