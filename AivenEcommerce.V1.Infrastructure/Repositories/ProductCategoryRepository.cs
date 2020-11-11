using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Infrastructure.Extensions;
using AivenEcommerce.V1.Infrastructure.Options;
using AivenEcommerce.V1.Infrastructure.Repositories.Base;

using Octokit;

namespace AivenEcommerce.V1.Infrastructure.Repositories
{
    public class ProductCategoryRepository : GitHubRepository<ProductCategory, Guid>, IProductCategoryRepository
    {
        private readonly IGitHubOptions _gitHubOptions;
        public ProductCategoryRepository(IGitHubClient githubClient, IGitHubOptions gitHubOptions) : base(githubClient)
        {
            _gitHubOptions = gitHubOptions;
        }

        public async Task<ProductCategory> GetByNameAsync(string productCategoryName)
        {
            var fileContent = await base.GetFileContentAsync(_gitHubOptions.ProductCategoryRepositoryId, "categories", productCategoryName);

            if (fileContent is null)
            {
                return null;
            }

            return fileContent.Content.Deserialize<ProductCategory>();
        }

        public override async Task<IEnumerable<ProductCategory>> GetAllAsync()
        {
            var files = await base.GetAllFilesWithContentAsync(_gitHubOptions.ProductCategoryRepositoryId, "categories");

            if (files is null)
            {
                return Enumerable.Empty<ProductCategory>();
            }

            return files.Select(x => x.Content.Deserialize<ProductCategory>());
        }

        public async Task<IEnumerable<ProductCategory>> GetAllNamesAsync()
        {
            var files = await base.GetAllFilesAsync(_gitHubOptions.ProductCategoryRepositoryId, "categories");

            if (files is null)
            {
                return Enumerable.Empty<ProductCategory>();
            }

            return files.Select(x => x.Content.Deserialize<ProductCategory>());
        }

        public override async Task<ProductCategory> CreateAsync(ProductCategory entity)
        {
            entity.Id = Guid.NewGuid();

            await base.CreateFileAsync(_gitHubOptions.ProductCategoryRepositoryId, "categories", entity.Name, entity.Serialize());

            return entity;
        }

        public override Task UpdateAsync(ProductCategory entityIn)
        {
            return base.UpdateFileAsync(_gitHubOptions.ProductCategoryRepositoryId, "categories", entityIn.Name, entityIn.Serialize());
        }

        public override Task RemoveAsync(ProductCategory entityIn)
        {
            return base.DeleteFileAsync(_gitHubOptions.ProductCategoryRepositoryId, "categories", entityIn.Name);
        }
    }
}
