using AivenEcommerce.V1.Modules.ImgBB.Options;
using AivenEcommerce.V1.Modules.ImgBB.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AivenEcommerce.V1.Modules.ImgBB.DependencyInjection.Extensions
{
    public static class ImgBbExtensions
    {
        public static IServiceCollection AddImgBb(this IServiceCollection services)
        {
            services.AddSingleton<IImgBbOptions, ImgBbOptions>(sp =>
            {
                IConfiguration configuration = sp.GetRequiredService<IConfiguration>();

                ImgBbOptions options = new();

                configuration.GetSection(nameof(ImgBbOptions)).Bind(options);

                return options;
            });

            services.AddHttpClient<IImgBbService, ImgBbService>();
            return services;
        }
    }
}
