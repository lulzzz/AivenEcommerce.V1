using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.Products;
using AivenEcommerce.V1.Domain.Shared.Paginations;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IProductRepository : IRepository<Product, string>
    {
        IEnumerable<Product> GetAvailableProducts();
        IEnumerable<Product> GetAvailableProducts(IEnumerable<string> products);
        IEnumerable<Product> GetAvailableProductsByCategory(string category);
        IEnumerable<Product> GetAvailableProductsByCategory(string category, string subcategory);
        IEnumerable<Product> GetAllProductsByCategory(string category);
        IEnumerable<Product> GetAllProductsByCategory(string category, string subcategory);
        IEnumerable<Product> GetProducts(IEnumerable<string> products);
        Product GetByName(string productName);
        Task UpdateCategoryName(string oldName, string newName);
        Task UpdateSubCategoryName(string oldName, string newName);
        Task InactiveProductsByCategory(string category);
        Task InactiveProductsByCategory(string category, string subcategory);
        Task<int> CountByCategory(string categoryName);
        Task<int> CountBySubCategory(string categoryName, string subcategoryName);
        Task<PagedData<Product>> GetAllAsync(ProductParameters parameters);

    }
}
