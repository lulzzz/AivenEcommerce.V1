using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Infrastructure.Options.Mongo;
using AivenEcommerce.V1.Infrastructure.Repositories.Base;

using MongoDB.Driver;
using MongoDB.Driver.Linq;

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

        public Task UpdateCategoryName(string oldName, string newName)
        {
            return base._collection.UpdateManyAsync(Builders<Product>.Filter.Eq(x => x.Category, oldName),
                 Builders<Product>.Update.Set(x => x.Category, newName));
        }

        public Task UpdateSubCategoryName(string oldName, string newName)
        {
            return base._collection.UpdateManyAsync(Builders<Product>.Filter.Eq(x => x.SubCategory, oldName),
                 Builders<Product>.Update.Set(x => x.SubCategory, newName));
        }

        public Task InactiveProductsByCategory(string category)
        {
            return base._collection.UpdateManyAsync(Builders<Product>.Filter.Eq(x => x.Category, category),
                 Builders<Product>.Update.Combine(Builders<Product>.Update.Set(x => x.IsActive, false), Builders<Product>.Update.Set(x => x.Category, string.Empty)));
        }

        public Task InactiveProductsByCategory(string category, string subcategory)
        {
            return base._collection.UpdateManyAsync(Builders<Product>.Filter.And(
                    Builders<Product>.Filter.Eq(x => x.Category, category),
                    Builders<Product>.Filter.Eq(x => x.SubCategory, subcategory)
                ),
                 Builders<Product>.Update.Combine(Builders<Product>.Update.Set(x => x.IsActive, false), Builders<Product>.Update.Set(x => x.SubCategory, string.Empty)));
        }

        public IEnumerable<Product> GetAvailableProductsByCategory(string category)
        {
            return base.GetQueryable().Where(x => x.IsActive && x.Stock > 0 && x.Category == category).ToList();
        }

        public IEnumerable<Product> GetAvailableProductsByCategory(string category, string subcategory)
        {
            return base.GetQueryable().Where(x => x.IsActive && x.Stock > 0 && x.Category == category && x.SubCategory == subcategory).ToList();
        }

        public IEnumerable<Product> GetAllProductsByCategory(string category)
        {
            return base.GetQueryable().Where(x => x.Category == category).ToList();
        }

        public IEnumerable<Product> GetAllProductsByCategory(string category, string subcategory)
        {
            return base.GetQueryable().Where(x => x.Category == category && x.SubCategory == subcategory).ToList();
        }

        public Task<int> CountByCategory(string categoryName)
        {
            return base.GetQueryable().CountAsync(x => x.Category == categoryName && x.IsActive);
        }

        public Task<int> CountBySubCategory(string categoryName, string subcategoryName)
        {
            return base.GetQueryable().CountAsync(x => x.Category == categoryName && x.SubCategory == subcategoryName && x.IsActive);
        }
    }
}
