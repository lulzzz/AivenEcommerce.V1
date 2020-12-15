using AivenEcommerce.V1.Domain.Caching;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Infrastructure.Extensions;
using AivenEcommerce.V1.Infrastructure.Repositories.Base;
using AivenEcommerce.V1.Modules.GitHub.Options;
using AivenEcommerce.V1.Modules.GitHub.Services;

using System;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Infrastructure.Repositories
{
    public class ProductOverviewRepository : GitHubRepository<ProductOverview, Guid>, IProductOverviewRepository
    {
        private readonly ICachedRepository _cachedRepository;

        public ProductOverviewRepository(IGitHubOptions options, IGitHubService gitHubService, ICachedRepository cachedRepository) : base(gitHubService, options.ProductOverviewRepositoryId, "products")
        {
            _cachedRepository = cachedRepository ?? throw new ArgumentNullException(nameof(cachedRepository));
        }

        public async Task<ProductOverview> GetByProduct(Product product)
        {
            return await _cachedRepository.GetOrSetAsync(new(nameof(ProductOverview), nameof(GetByProduct), product.Id),

                       async () =>
                       {
                           var file = await base.GithubService.GetFileContentAsync(base.RepositoryId, base.Path, product.Id);
                           return file.Content.Deserialize<ProductOverview>();

                       }
                    );
        }

        public override async Task<ProductOverview> CreateAsync(ProductOverview entity)
        {
            entity.Id = Guid.NewGuid();
            await base.GithubService.CreateFileAsync(base.RepositoryId, base.Path, entity.ProductId, entity.Serialize());

            return entity;
        }

        public override async Task UpdateAsync(ProductOverview entityIn)
        {
            await base.GithubService.UpdateFileAsync(base.RepositoryId, base.Path, entityIn.ProductId, entityIn.Serialize());
        }

        public override Task RemoveAsync(ProductOverview entityIn)
        {
            return base.GithubService.DeleteFileAsync(base.RepositoryId, base.Path, entityIn.ProductId);
        }
    }
}
