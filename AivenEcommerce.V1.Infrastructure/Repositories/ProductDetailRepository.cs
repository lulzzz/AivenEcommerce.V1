using System.Collections.Generic;
using System.Linq;

using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Enums;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Infrastructure.Options.Mongo;
using AivenEcommerce.V1.Infrastructure.Repositories.Base;

namespace AivenEcommerce.V1.Infrastructure.Repositories
{
    public class ProductDetailRepository : MongoRepository<ProductDetail>, IProductDetailRepository
    {
        public ProductDetailRepository(IMongoProductDetailOptions options) : base(options)
        {
        }

        public IEnumerable<ProductDetail> GetByProduct(Product product)
        {
            return base.GetQueryable().Where(x => x.ProductId == product.Id).ToList();
        }

        public IEnumerable<ProductDetail> GetByProduct(Product product, ProductDetailType type)
        {
            return base.GetQueryable().Where(x => x.ProductId == product.Id && x.Type == type).ToList();
        }
    }
}
