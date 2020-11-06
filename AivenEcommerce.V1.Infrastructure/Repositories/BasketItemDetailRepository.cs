using System.Collections.Generic;
using System.Linq;

using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Infrastructure.Options.Mongo;
using AivenEcommerce.V1.Infrastructure.Repositories.Base;

namespace AivenEcommerce.V1.Infrastructure.Repositories
{
    public class BasketItemDetailRepository : MongoRepository<BasketItemDetail>, IBasketItemDetailRepository
    {
        public BasketItemDetailRepository(IMongoBasketItemDetailOptions options) : base(options)
        {
        }

        public IEnumerable<BasketItemDetail> GetBasketItems(BasketItem basketItem)
        {
            return base.GetQueryable().Where(x => x.BasketItemId == basketItem.Id).ToList();
        }
    }
}
