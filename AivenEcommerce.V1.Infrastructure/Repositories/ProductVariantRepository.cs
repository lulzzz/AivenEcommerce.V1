using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Infrastructure.Extensions;
using AivenEcommerce.V1.Infrastructure.Repositories.Base;
using AivenEcommerce.V1.Modules.GitHub.Options;
using AivenEcommerce.V1.Modules.GitHub.Services;

namespace AivenEcommerce.V1.Infrastructure.Repositories
{
    public class ProductVariantRepository : GitHubRepository<ProductVariant, Guid>, IProductVariantRepository
    {
        private readonly IGitHubOptions _gitHubOptions;
        public ProductVariantRepository(IGitHubService githubService, IGitHubOptions gitHubOptions) : base(githubService)
        {
            _gitHubOptions = gitHubOptions;
        }

        public override async Task<ProductVariant> CreateAsync(ProductVariant entity)
        {
            entity.Id = Guid.NewGuid();

            await base._githubService.CreateFileAsync(_gitHubOptions.ProductVariantRepositoryId, $"variants/{entity.ProductId}", entity.Name, entity.Serialize());

            return entity;
        }

        public override Task UpdateAsync(ProductVariant entityIn)
        {
            return base._githubService.UpdateFileAsync(_gitHubOptions.ProductVariantRepositoryId, $"variants/{entityIn.ProductId}", entityIn.Name, entityIn.Serialize());
        }

        public override Task RemoveAsync(ProductVariant entityIn)
        {
            return base._githubService.DeleteFileAsync(_gitHubOptions.ProductVariantRepositoryId, $"variants/{entityIn.ProductId}", entityIn.Name);
        }

        public async Task<IEnumerable<ProductVariant>> GetByProduct(Product product)
        {
            var files = await base._githubService.GetAllFilesWithContentAsync(_gitHubOptions.ProductVariantRepositoryId, $"variants/{product.Id}");

            if (files is null)
            {
                return Enumerable.Empty<ProductVariant>();
            }

            return files.Select(x => x.Content.Deserialize<ProductVariant>());
        }

        public async Task<ProductVariant> GetByNameAsync(Product product, string variantName)
        {
            var fileContent = await base._githubService.GetFileContentAsync(_gitHubOptions.ProductVariantRepositoryId, $"variants/{product.Id}", variantName);

            if (fileContent is null)
            {
                return null;
            }

            return fileContent.Content.Deserialize<ProductVariant>();
        }
    }
}
