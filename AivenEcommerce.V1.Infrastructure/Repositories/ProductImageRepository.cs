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
    public class ProductImageRepository : GitHubRepository<ProductImage, Guid>, IProductImageRepository
    {
        private readonly IGitHubOptions _gitHubOptions;
        public ProductImageRepository(IGitHubClient githubClient, IGitHubOptions gitHubOptions) : base(githubClient)
        {
            _gitHubOptions = gitHubOptions;
        }

        public async Task<IEnumerable<ProductImage>> UpdateProductImages(IEnumerable<ProductImage> productImages)
        {
            string json = productImages.Serialize();
            await UpdateFileAsync(_gitHubOptions.ProductImageRepositoryId, "products", productImages.First().ProductId, json);

            return productImages;
        }

        public async Task<IEnumerable<ProductImage>> GetProductImages(Product product)
        {
            var file = await GetFileContentAsync(_gitHubOptions.ProductImageRepositoryId, "products", product.Id);

            if (file is null)
            {
                return Enumerable.Empty<ProductImage>();
            }

            return file.Content.Deserialize<IEnumerable<ProductImage>>();
        }

        public async Task<IEnumerable<ProductImage>> CreateProductImages(IEnumerable<ProductImage> productImages)
        {
            string json = productImages.Serialize();
            await CreateFileAsync(_gitHubOptions.ProductImageRepositoryId, "products", productImages.First().ProductId, json);

            return productImages;
        }
    }
}
