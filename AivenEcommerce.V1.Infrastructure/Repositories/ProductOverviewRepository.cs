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
    public class ProductOverviewRepository : GitHubRepository<ProductOverview, Guid>, IProductOverviewRepository
    {
        private readonly IGitHubOptions _gitHubOptions;

        public ProductOverviewRepository(IGitHubOptions gitHubOptions, IGitHubClient gitHubClient) : base(gitHubClient)
        {
            _gitHubOptions = gitHubOptions ?? throw new ArgumentNullException(nameof(gitHubOptions));
        }

        public async Task<ProductOverview> GetByProduct(Product product)
        {
            var file = await base.GetFileContentAsync(_gitHubOptions.ProductOverviewRepositoryId, "products", product.Id);
            return file.Content.Deserialize<ProductOverview>();
        }

        public override async Task<ProductOverview> CreateAsync(ProductOverview entity)
        {
            entity.Id = Guid.NewGuid();
            await base.CreateFileAsync(_gitHubOptions.ProductOverviewRepositoryId, "products", entity.ProductId, entity.Serialize());

            return entity;
        }

        public override async Task UpdateAsync(ProductOverview entityIn)
        {
            await base.UpdateFileAsync(_gitHubOptions.ProductOverviewRepositoryId, "products", entityIn.ProductId, entityIn.Serialize());
        }

        public override Task RemoveAsync(ProductOverview entityIn)
        {
            return base.DeleteFileAsync(_gitHubOptions.ProductOverviewRepositoryId, "products", entityIn.ProductId);
        }
    }
}
