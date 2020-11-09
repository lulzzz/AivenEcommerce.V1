using System.Collections.Generic;

using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IProductRepository : IRepository<Product, string>
    {
        IEnumerable<Product> GetAvailableProducts();
        Product? GetByName(string productName);
    }
}
