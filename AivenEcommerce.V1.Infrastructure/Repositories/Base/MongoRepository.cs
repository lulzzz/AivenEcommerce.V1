using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities.Base;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Infrastructure.Options.Mongo;

using MongoDB.Driver;

namespace AivenEcommerce.V1.Infrastructure.Repositories.Base
{
    public class MongoRepository<T> : IRepository<T> where T : IEntity<string>
    {
        private readonly IMongoCollection<T> _collection;

        public MongoRepository(IMongoOptions options)
        {
            var client = new MongoClient(options.ConnectionString);
            var database = client.GetDatabase(options.DatabaseName);

            _collection = database.GetCollection<T>(options.CollectionName);
        }

        protected IQueryable<T> GetQueryable()
        {
            return _collection.AsQueryable();
        }

        public IEnumerable<T> GetAll()
        {
            return GetQueryable().ToList();
        }

        public Task<T> GetAsync(string id)
        {
            return _collection.Find(x => x.Id == id).SingleAsync();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public Task UpdateAsync(T entityIn) =>
            _collection.ReplaceOneAsync(x => x.Id == entityIn.Id, entityIn);

        public Task RemoveAsync(T entityIn) =>
            _collection.DeleteOneAsync(x => x.Id == entityIn.Id);

        public Task RemoveAsync(string id) =>
            _collection.DeleteOneAsync(x => x.Id == id);
    }
}
