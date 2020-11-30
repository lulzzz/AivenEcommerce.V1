using AivenEcommerce.V1.Domain.Entities.Base;
using AivenEcommerce.V1.Domain.Shared.Common;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IRepository<T, K> : IScopedService where T : IEntity<K>
    {
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(K id);
        Task RemoveAsync(K id);
        Task RemoveAsync(T entityIn);
        Task RemoveAllAsync();
        Task UpdateAsync(T entityIn);
    }
}