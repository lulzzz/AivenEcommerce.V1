using System.Collections.Generic;

using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Enums;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IProductDetailRepository : IRepository<ProductDetail, string>
    {
        IEnumerable<ProductDetail> GetByProduct(Product product);
        IEnumerable<ProductDetail> GetByProduct(Product product, ProductDetailType type);
    }
}
