using System.Collections.Generic;
using System.Linq;

using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Infrastructure.Options.Mongo;
using AivenEcommerce.V1.Infrastructure.Repositories.Base;

namespace AivenEcommerce.V1.Infrastructure.Repositories
{
    public class ProductRepository : MongoRepository<Product>, IProductRepository
    {
        public ProductRepository(IMongoProductOptions options) : base(options)
        {
        }

        public IEnumerable<Product> GetAvailableProducts()
        {
            return base.GetQueryable().Where(x => x.IsActive && x.Stock > 0).ToList();
        }

        public Product? GetByName(string productName)
        {
            return base.GetQueryable().Where(x => x.Name == productName).SingleOrDefault();
        }
    }
}
