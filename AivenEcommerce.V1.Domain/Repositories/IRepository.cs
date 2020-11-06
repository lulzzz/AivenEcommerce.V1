using System.Collections.Generic;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities.Base;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IRepository<T> where T : IEntity<string>
    {
        Task<T> CreateAsync(T entity);
        IEnumerable<T> GetAll();
        Task<T> GetAsync(string id);
        Task RemoveAsync(string id);
        Task RemoveAsync(T entityIn);
        Task UpdateAsync(T entityIn);
    }
}