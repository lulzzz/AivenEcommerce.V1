using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IProductImageRepository : IRepository<ProductImage, Guid>
    {
        Task<IEnumerable<ProductImage>> GetProductImages(Product product);
        Task<IEnumerable<ProductImage>> UpdateProductImages(IEnumerable<ProductImage> productImages);
        Task<IEnumerable<ProductImage>> CreateProductImages(IEnumerable<ProductImage> productImages);
    }
}
