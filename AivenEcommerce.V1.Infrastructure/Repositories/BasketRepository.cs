using System.Collections.Generic;
using System.Linq;

using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Enums;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Infrastructure.Options.Mongo;
using AivenEcommerce.V1.Infrastructure.Repositories.Base;

namespace AivenEcommerce.V1.Infrastructure.Repositories
{
    public class BasketRepository : MongoRepository<Basket>, IBasketRepository
    {
        public BasketRepository(IMongoBasketOptions options) : base(options)
        {
        }

        public Basket GetBasketOpenByUser(User user)
        {
            return base.GetQueryable().Where(x => x.UserId == user.Id && x.Status == BasketStatus.Open).SingleOrDefault();
        }

        public IEnumerable<Basket> GetBasketsByUser(User user)
        {
            return base.GetQueryable().Where(x => x.UserId == user.Id).ToList();
        }
    }
}
