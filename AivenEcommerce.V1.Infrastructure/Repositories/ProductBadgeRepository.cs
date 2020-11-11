using System;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Infrastructure.Extensions;
using AivenEcommerce.V1.Infrastructure.Options;
using AivenEcommerce.V1.Infrastructure.Repositories.Base;

using Octokit;

namespace AivenEcommerce.V1.Infrastructure.Repositories
{
    public class ProductBadgeRepository : GitHubRepository<ProductBadge, Guid>, IProductBadgeRepository
    {
        private readonly IGitHubOptions _gitHubOptions;
        public ProductBadgeRepository(IGitHubClient githubClient, IGitHubOptions gitHubOptions) : base(githubClient)
        {
            _gitHubOptions = gitHubOptions;
        }

        public async Task<ProductBadge> GetByProduct(Product product)
        {
            var file = await base.GetFileContentAsync(_gitHubOptions.ProductBadgeRepositoryId, "products", product.Id);

            return file?.Content?.Deserialize<ProductBadge>();
        }

        public override async Task<ProductBadge> CreateAsync(ProductBadge entity)
        {
            entity.Id = Guid.NewGuid();
            await base.CreateFileAsync(_gitHubOptions.ProductBadgeRepositoryId, "products", entity.ProductId, entity.Serialize());

            return entity;
        }

        public override async Task UpdateAsync(ProductBadge entityIn)
        {
            await base.UpdateFileAsync(_gitHubOptions.ProductBadgeRepositoryId, "products", entityIn.ProductId, entityIn.Serialize());
        }
    }
}
