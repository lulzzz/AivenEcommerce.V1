using AivenEcommerce.V1.Application.Extensions;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Shared.Dtos.Orders;
using AivenEcommerce.V1.Domain.Shared.Paginations;
using AivenEcommerce.V1.Infrastructure.Options.Mongo;
using AivenEcommerce.V1.Infrastructure.Repositories.Base;

using MongoDB.Bson;
using MongoDB.Driver;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Infrastructure.Repositories
{
    public class OrderRepository : MongoRepository<Order>, IOrderRepository
    {
        public OrderRepository(IMongoOrderOptions options) : base(options)
        {
        }

        public IEnumerable<Order> GetOrdersByUser(User user)
        {
            //TODO: fieohfoi
            return base.GetQueryable().Where(x => true).ToList();
        }

        public async Task<PagedData<Order>> GetAllAsync(OrderParameters parameters)
        {
            var filterDefinitions = Enumerable.Empty<FilterDefinition<Order>>();

            if (parameters.Status.HasValue)
            {
                filterDefinitions = filterDefinitions.Append(Builders<Order>.Filter.Eq(x => x.Status, parameters.Status.Value));
            }

            var filterComplete = Builders<Order>.Filter.And(filterDefinitions);

            var taskCount = base._collection.Find(filterComplete).CountDocumentsAsync();

            var findFluent = base._collection.Find(filterComplete);


            if (parameters.SortDirection is not Domain.Shared.Enums.SortDirection.None)
            {
                var sort = parameters.SortDirection switch
                {
                    Domain.Shared.Enums.SortDirection.Asc => Builders<Order>.Sort.Ascending(parameters.SortColumn),
                    Domain.Shared.Enums.SortDirection.Desc => Builders<Order>.Sort.Descending(parameters.SortColumn),
                    _ => Builders<Order>.Sort.Ascending(parameters.SortColumn)
                };

                findFluent = findFluent.Sort(sort);
            }

            if (parameters.PageSize.HasValue)
            {
                findFluent = findFluent.Skip(parameters.CalculateSkip()).Limit(parameters.PageSize);
            }

            var products = await findFluent.ToListAsync();

            PagedData<Order> paged = new(products, await taskCount);

            return paged;

        }
    }
}
