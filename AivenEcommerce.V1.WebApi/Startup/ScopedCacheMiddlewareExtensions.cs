using AivenEcommerce.V1.WebApi.Middlewares;

using Microsoft.AspNetCore.Builder;

namespace AivenEcommerce.V1.WebApi.Startup
{
    public static class ScopedCacheMiddlewareExtensions
    {
        public static IApplicationBuilder UseScopedCache(this IApplicationBuilder app)
        {
            app.UseMiddleware<ScopedCacheMiddleware>();
            return app;
        }
    }
}
