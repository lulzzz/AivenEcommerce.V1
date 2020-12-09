using AivenEcommerce.V1.WebApi.Middlewares;

using Microsoft.AspNetCore.Builder;

namespace AivenEcommerce.V1.WebApi.Startup
{
    public static class IPSafeMiddleWareExtensions
    {
        public static IApplicationBuilder UseIPSafe(this IApplicationBuilder app)
        {
            app.UseMiddleware<IPSafeMiddleWare>();
            return app;
        }
    }
}
