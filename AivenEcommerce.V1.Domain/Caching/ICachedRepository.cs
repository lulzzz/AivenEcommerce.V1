
using AivenEcommerce.V1.Domain.Shared.Common;

using System;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Caching
{
    public interface ICachedRepository : IService
    {
        T GetOrSet<T>(ScopedCacheKey cacheKey, Func<T> getItemCallback) where T : class;
        Task<T> GetOrSetAsync<T>(ScopedCacheKey cacheKey, Func<Task<T>> getItemCallback) where T : class;
        void Reset();
    }
}
