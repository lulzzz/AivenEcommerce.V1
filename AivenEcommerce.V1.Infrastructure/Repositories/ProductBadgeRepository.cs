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
    public class ProductBadgeRepository : GitHubRepository<ProductBadge, Guid>, IProductBadgeRepository
    {
        private readonly ICachedRepository _cachedRepository;

        public ProductBadgeRepository(IGitHubService githubService, IGitHubOptions options, ICachedRepository cachedRepository) : base(githubService, options.ProductBadgeRepositoryId, "products")
        {
            _cachedRepository = cachedRepository ?? throw new ArgumentNullException(nameof(cachedRepository));
        }

        public async Task<ProductBadge> GetByProduct(Product product)
        {
            return await _cachedRepository.GetOrSetAsync(new(nameof(ProductBadge), nameof(GetByProduct), product.Id),

                       async () =>
                       {
                           var file = await base.GithubService.GetFileContentAsync(base.RepositoryId, base.Path, product.Id);

                           return file?.Content?.Deserialize<ProductBadge>();

                       }
                    );
        }

        public override async Task<ProductBadge> CreateAsync(ProductBadge entity)
        {
            entity.Id = Guid.NewGuid();
            await base.GithubService.CreateFileAsync(base.RepositoryId, base.Path, entity.ProductId, entity.Serialize());

            return entity;
        }

        public override Task UpdateAsync(ProductBadge entityIn)
        {
            return base.GithubService.UpdateFileAsync(base.RepositoryId, base.Path, entityIn.ProductId, entityIn.Serialize());
        }

        public override Task RemoveAsync(ProductBadge entityIn)
        {
            return base.GithubService.DeleteFileAsync(base.RepositoryId, base.Path, entityIn.ProductId);
        }
    }
}
