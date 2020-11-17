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
    public class ProductImageRepository : GitHubRepository<ProductImage, Guid>, IProductImageRepository
    {
        private readonly IGitHubOptions _gitHubOptions;
        public ProductImageRepository(IGitHubService githubService, IGitHubOptions gitHubOptions) : base(githubService)
        {
            _gitHubOptions = gitHubOptions;
        }

        public async Task<IEnumerable<ProductImage>> UpdateProductImages(IEnumerable<ProductImage> productImages)
        {
            string json = productImages.Serialize();
            await base._githubService.UpdateFileAsync(_gitHubOptions.ProductImageRepositoryId, "products", productImages.First().ProductId, json);

            return productImages;
        }

        public async Task<IEnumerable<ProductImage>> GetProductImages(Product product)
        {
            var file = await base._githubService.GetFileContentAsync(_gitHubOptions.ProductImageRepositoryId, "products", product.Id);

            if (file is null)
            {
                return Enumerable.Empty<ProductImage>();
            }

            return file.Content.Deserialize<IEnumerable<ProductImage>>();
        }

        public async Task<IEnumerable<ProductImage>> CreateProductImages(IEnumerable<ProductImage> productImages)
        {
            string json = productImages.Serialize();
            await base._githubService.CreateFileAsync(_gitHubOptions.ProductImageRepositoryId, "products", productImages.First().ProductId, json);

            return productImages;
        }

        public override Task RemoveAsync(ProductImage entityIn)
        {
            return base._githubService.DeleteFileAsync(_gitHubOptions.ProductImageRepositoryId, "products", entityIn.ProductId);
        }
    }
}
