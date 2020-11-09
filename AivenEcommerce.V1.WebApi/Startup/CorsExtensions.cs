
using Microsoft.AspNetCore.Builder;

namespace AivenEcommerce.V1.WebApi.Startup
{
    public static class CorsExtensions
    {
        public static IApplicationBuilder UseAllowAnyCors(this IApplicationBuilder app)
        {
            return app.UseCors(builder => builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod().AllowAnyHeader());
        }
    }
}
