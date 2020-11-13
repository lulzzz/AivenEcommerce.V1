using System.Collections.Generic;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IProductRepository : IRepository<Product, string>
    {
        IEnumerable<Product> GetAvailableProducts();
        IEnumerable<Product> GetAvailableProductsByCategory(string category);
        IEnumerable<Product> GetAvailableProductsByCategory(string category, string subcategory);
        IEnumerable<Product> GetAllProductsByCategory(string category);
        IEnumerable<Product> GetAllProductsByCategory(string category, string subcategory);
        Product? GetByName(string productName);
        Task UpdateCategoryName(string oldName, string newName);
        Task UpdateSubCategoryName(string oldName, string newName);
        Task InactiveProductsByCategory(string category);
        Task InactiveProductsByCategory(string category, string subcategory);
    }
}
