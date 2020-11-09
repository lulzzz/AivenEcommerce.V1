
using AivenEcommerce.V1.Modules.ImgBB.Options;
using AivenEcommerce.V1.Modules.ImgBB.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AivenEcommerce.V1.WebApi.Startup
{
    public static class ImgBbExtensions
    {
        public static IServiceCollection AddImgBb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<IImgBbOptions, ImgBbOptions>(configuration);
            services.AddHttpClient<IImgBbService, ImgBbService>();
            return services;
        }
    }
}
