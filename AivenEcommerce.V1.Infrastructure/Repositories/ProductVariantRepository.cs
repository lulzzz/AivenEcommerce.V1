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
        public ProductVariantRepository(IGitHubService githubService, IGitHubOptions options) : base(githubService, options.ProductVariantRepositoryId)
        {
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
            var files = await base.GithubService.GetAllFilesWithContentAsync(base.RepositoryId, $"variants/{product.Id}");

            if (files is null)
            {
                return Enumerable.Empty<ProductVariant>();
            }

            return files.Select(x => x.Content.Deserialize<ProductVariant>());
        }

        public async Task<ProductVariant> GetByNameAsync(Product product, string variantName)
        {
            var fileContent = await base.GithubService.GetFileContentAsync(base.RepositoryId, $"variants/{product.Id}", variantName);

            if (fileContent is null)
            {
                return null;
            }

            return fileContent.Content.Deserialize<ProductVariant>();
        }
    }
}
