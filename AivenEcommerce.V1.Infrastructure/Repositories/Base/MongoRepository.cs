using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities.Base;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Infrastructure.Options.Mongo;

using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace AivenEcommerce.V1.Infrastructure.Repositories.Base
{
    public class MongoRepository<T> : IRepository<T, string> where T : IEntity<string>
    {
        protected readonly IMongoCollection<T> _collection;

        public MongoRepository(IMongoOptions options)
        {
            var client = new MongoClient(options.ConnectionString);
            var database = client.GetDatabase(options.DatabaseName);

            _collection = database.GetCollection<T>(options.CollectionName);
        }

        protected IMongoQueryable<T> GetQueryable()
        {
            return _collection.AsQueryable();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.AsQueryable().ToListAsync();
        }

        public Task<T> GetAsync(string id)
        {
            return _collection.Find(x => x.Id == id).SingleOrDefaultAsync();
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

        public Task RemoveAllAsync()
        {
            return _collection.DeleteManyAsync(x => true);
        }
    }
}
