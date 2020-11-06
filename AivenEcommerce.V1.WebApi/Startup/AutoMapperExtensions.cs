
using Microsoft.Extensions.DependencyInjection;

namespace AivenEcommerce.V1.WebApi.Startup
{
    public static class AutoMapperExtensions
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper();

            return services;
        }
    }
}
