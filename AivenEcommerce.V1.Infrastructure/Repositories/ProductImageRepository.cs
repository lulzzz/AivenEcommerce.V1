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
    public class ProductImageRepository : GitHubRepository<ProductImage, Guid>, IProductImageRepository
    {
        public ProductImageRepository(IGitHubService githubService, IGitHubOptions options) : base(githubService, options.ProductImageRepositoryId, "products")
        {
        }

        public async Task<IEnumerable<ProductImage>> UpdateProductImages(IEnumerable<ProductImage> productImages)
        {
            string json = productImages.Serialize();
            await base.GithubService.UpdateFileAsync(base.RepositoryId, base.Path, productImages.First().ProductId, json);

            return productImages;
        }

        public async Task<IEnumerable<ProductImage>> GetProductImages(Product product)
        {
            var file = await base.GithubService.GetFileContentAsync(base.RepositoryId, base.Path, product.Id);

            if (file is null)
            {
                return Enumerable.Empty<ProductImage>();
            }

            return file.Content.Deserialize<IEnumerable<ProductImage>>();
        }

        public async Task<IEnumerable<ProductImage>> CreateProductImages(IEnumerable<ProductImage> productImages)
        {
            string json = productImages.Serialize();
            await base.GithubService.CreateFileAsync(base.RepositoryId, base.Path, productImages.First().ProductId, json);

            return productImages;
        }

        public override Task RemoveAsync(ProductImage entityIn)
        {
            return base.GithubService.DeleteFileAsync(base.RepositoryId, base.Path, entityIn.ProductId);
        }
    }
}
