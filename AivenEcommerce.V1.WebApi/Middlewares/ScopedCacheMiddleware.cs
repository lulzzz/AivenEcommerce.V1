using AivenEcommerce.V1.Domain.Caching;

using Microsoft.AspNetCore.Http;

using System;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.WebApi.Middlewares
{
    public class ScopedCacheMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ICachedRepository _cachedRepository;

        public ScopedCacheMiddleware(RequestDelegate next, ICachedRepository cachedRepository)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _cachedRepository = cachedRepository ?? throw new ArgumentNullException(nameof(cachedRepository));
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            _cachedRepository.Reset();
        }
    }
}
