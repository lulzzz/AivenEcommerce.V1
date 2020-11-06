using System.Collections.Generic;
using System.Linq;

using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Infrastructure.Options.Mongo;
using AivenEcommerce.V1.Infrastructure.Repositories.Base;

namespace AivenEcommerce.V1.Infrastructure.Repositories
{
    public class BasketItemRepository : MongoRepository<BasketItem>, IBasketItemRepository
    {
        public BasketItemRepository(IMongoBasketItemOptions options) : base(options)
        {
        }

        public IEnumerable<BasketItem> GetBasketItems(Basket basket)
        {
            return base.GetQueryable().Where(x => x.BasketId == basket.Id).ToList();
        }

        public IEnumerable<BasketItem> GetBasketItems(Basket basket, Product product)
        {
            return base.GetQueryable().Where(x => x.BasketId == basket.Id && x.ProductId == product.Id).ToList();
        }
    }
}
