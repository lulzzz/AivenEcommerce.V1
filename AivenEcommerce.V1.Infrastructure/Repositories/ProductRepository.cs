﻿using AivenEcommerce.V1.Application.Extensions;
using AivenEcommerce.V1.Domain.Caching;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Shared.Dtos.Products;
using AivenEcommerce.V1.Domain.Shared.Paginations;
using AivenEcommerce.V1.Infrastructure.Extensions;
using AivenEcommerce.V1.Infrastructure.Options.Mongo;
using AivenEcommerce.V1.Infrastructure.Repositories.Base;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Infrastructure.Repositories
{
    public class ProductRepository : MongoRepository<Product>, IProductRepository
    {
        private readonly ICachedRepository _cachedRepository;

        public ProductRepository(IMongoProductOptions options, ICachedRepository cachedRepository) : base(options)
        {
            _cachedRepository = cachedRepository ?? throw new ArgumentNullException(nameof(cachedRepository));
        }

        public IEnumerable<Product> GetAvailableProducts() =>

        _cachedRepository.GetOrSet(new(nameof(Product), nameof(GetAvailableProducts)),
                       () => base.GetQueryable().Where(x => x.IsActive && x.Stock > 0)
            );


        public IEnumerable<Product> GetAvailableProducts(IEnumerable<string> products) =>

            _cachedRepository.GetOrSet(new(nameof(Product), nameof(GetAvailableProducts), products.Serialize()),
                       () => base.GetQueryable().Where(x => products.Contains(x.Id) && x.IsActive && x.Stock > 0)
            );

        public Product GetByName(string productName) =>

            _cachedRepository.GetOrSet(new(nameof(Product), nameof(GetByName), productName),

                () => base.GetQueryable().Where(x => x.Name == productName).SingleOrDefault()
            );


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

        public IEnumerable<Product> GetProducts(IEnumerable<string> products)
        {
            return base.GetQueryable().Where(x => products.Contains(x.Id)).ToList();
        }

        public async Task<PagedData<Product>> GetAllAsync(ProductParameters parameters)
        {
            var filterDefinitions = Enumerable.Empty<FilterDefinition<Product>>();

            FilterDefinition<Product> filter = new BsonDocument();

            if (parameters.IsActive.HasValue)
            {
                filterDefinitions = filterDefinitions.Append(Builders<Product>.Filter.Eq(x => x.IsActive, parameters.IsActive.Value));
            }

            if (filterDefinitions.Any())
            {
                filter = Builders<Product>.Filter.And(filterDefinitions);
            }

            var taskCount = base._collection.Find(filter).CountDocumentsAsync();

            var findFluent = base._collection.Find(filter);

            if (parameters.SortDirection is not Domain.Shared.Enums.SortDirection.None)
            {
                var sort = parameters.SortDirection switch
                {
                    Domain.Shared.Enums.SortDirection.Asc => Builders<Product>.Sort.Ascending(parameters.SortColumn),
                    Domain.Shared.Enums.SortDirection.Desc => Builders<Product>.Sort.Descending(parameters.SortColumn),
                    _ => Builders<Product>.Sort.Ascending(parameters.SortColumn)
                };

                findFluent = findFluent.Sort(sort);
            }

            if (parameters.PageSize.HasValue)
            {
                findFluent = findFluent.Skip(parameters.CalculateSkip()).Limit(parameters.PageSize);
            }

            var products = await findFluent.ToListAsync();

            PagedData<Product> paged = new(products, await taskCount);

            return paged;

        }
    }
}
