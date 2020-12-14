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
    public class ProductCategoryRepository : GitHubRepository<ProductCategory, Guid>, IProductCategoryRepository
    {
        private readonly ICachedRepository _cachedRepository;

        public ProductCategoryRepository(IGitHubService githubService, IGitHubOptions options, ICachedRepository cachedRepository) : base(githubService, options.ProductCategoryRepositoryId, "categories")
        {
            _cachedRepository = cachedRepository ?? throw new ArgumentNullException(nameof(cachedRepository));
        }

        public async Task<ProductCategory> GetByNameAsync(string productCategoryName)
        {
            return await _cachedRepository.GetOrSetAsync(new(nameof(ProductCategory), nameof(GetByNameAsync), productCategoryName),

                        async () =>
                        {
                            var fileContent = await base.GithubService.GetFileContentAsync(base.RepositoryId, base.Path, productCategoryName);

                            if (fileContent is null)
                            {
                                return null;
                            }

                            return fileContent.Content.Deserialize<ProductCategory>();
                        }
                    );
        }

        public async Task<IEnumerable<ProductCategory>> GetAllNamesAsync()
        {
            return await _cachedRepository.GetOrSetAsync(new(nameof(ProductCategory), nameof(GetAllNamesAsync), nameof(GetAllNamesAsync)),

                    async () =>
                    {

                        var files = await base.GithubService.GetAllFilesAsync(base.RepositoryId, base.Path);

                        if (files is null)
                        {
                            return Enumerable.Empty<ProductCategory>();
                        }

                        return files.Select(x => x.Content.Deserialize<ProductCategory>());
                    }
             );
        }

        public override async Task<IEnumerable<ProductCategory>> GetAllAsync()
        {
            return await _cachedRepository.GetOrSetAsync(new(nameof(ProductCategory), nameof(GetAllAsync)), () => base.GetAllAsync());
        }

        public override async Task<ProductCategory> CreateAsync(ProductCategory entity)
        {
            entity.Id = Guid.NewGuid();

            await base.GithubService.CreateFileAsync(base.RepositoryId, base.Path, entity.Name, entity.Serialize());

            return entity;
        }

        public override Task UpdateAsync(ProductCategory entityIn)
        {
            return base.GithubService.UpdateFileAsync(base.RepositoryId, base.Path, entityIn.Name, entityIn.Serialize());
        }

        public override Task RemoveAsync(ProductCategory entityIn)
        {
            return base.GithubService.DeleteFileAsync(base.RepositoryId, base.Path, entityIn.Name);
        }
    }
}
