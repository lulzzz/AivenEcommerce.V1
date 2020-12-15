using AivenEcommerce.V1.Domain.Caching;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Infrastructure.Extensions;
using AivenEcommerce.V1.Infrastructure.Repositories.Base;
using AivenEcommerce.V1.Modules.GitHub.Options;
using AivenEcommerce.V1.Modules.GitHub.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Infrastructure.Repositories
{
    public class ProductVariantRepository : GitHubRepository<ProductVariant, Guid>, IProductVariantRepository
    {
        private readonly ICachedRepository _cachedRepository;

        public ProductVariantRepository(IGitHubService githubService, IGitHubOptions options, ICachedRepository cachedRepository) : base(githubService, options.ProductVariantRepositoryId)
        {
            _cachedRepository = cachedRepository ?? throw new ArgumentNullException(nameof(cachedRepository));
        }

        public override async Task<ProductVariant> CreateAsync(ProductVariant entity)
        {
            entity.Id = Guid.NewGuid();

            await base.GithubService.CreateFileAsync(base.RepositoryId, $"variants/{entity.ProductId}", entity.Name, entity.Serialize());

            return entity;
        }

        public override Task UpdateAsync(ProductVariant entityIn)
        {
            return base.GithubService.UpdateFileAsync(base.RepositoryId, $"variants/{entityIn.ProductId}", entityIn.Name, entityIn.Serialize());
        }

        public override Task RemoveAsync(ProductVariant entityIn)
        {
            return base.GithubService.DeleteFileAsync(base.RepositoryId, $"variants/{entityIn.ProductId}", entityIn.Name);
        }

        public async Task<IEnumerable<ProductVariant>> GetByProduct(Product product)
        {
            return await _cachedRepository.GetOrSetAsync(new(nameof(ProductVariant), nameof(GetByProduct), product.Id),

                       async () =>
                       {
                           var files = await base.GithubService.GetAllFilesWithContentAsync(base.RepositoryId, $"variants/{product.Id}");

                           if (files is null)
                           {
                               return Enumerable.Empty<ProductVariant>();
                           }

                           return files.Select(x => x.Content.Deserialize<ProductVariant>());

                       }
                    );
        }

        public async Task<ProductVariant> GetByNameAsync(Product product, string variantName)
        {
            return await _cachedRepository.GetOrSetAsync(new(nameof(ProductVariant), nameof(GetByNameAsync), product.Id + variantName),

                       async () =>
                       {

                           var fileContent = await base.GithubService.GetFileContentAsync(base.RepositoryId, $"variants/{product.Id}", variantName);

                           if (fileContent is null)
                           {
                               return null;
                           }

                           return fileContent.Content.Deserialize<ProductVariant>();
                       }
                    );
        }
    }
}
