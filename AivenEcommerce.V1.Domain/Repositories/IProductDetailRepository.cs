using System.Collections.Generic;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Enums;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IProductDetailRepository
    {
        Task<ProductDetail> GetProductDetail(int id);
        Task<IEnumerable<ProductDetail>> GetByProduct(Product product);
        Task<IEnumerable<ProductDetail>> GetByProduct(Product product, ProductDetailType type);
    }
}
