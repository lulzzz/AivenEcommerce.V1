using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Infrastructure.Options.Mongo;
using AivenEcommerce.V1.Infrastructure.Repositories.Base;

namespace AivenEcommerce.V1.Infrastructure.Repositories
{
    public class SaleRepository : MongoRepository<Sale>, ISaleRepository
    {
        public SaleRepository(IMongoSaleOptions options) : base(options)
        {
        }
    }
}
