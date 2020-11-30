using AivenEcommerce.V1.Modules.PayPal.Options;
using AivenEcommerce.V1.Modules.PayPal.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AivenEcommerce.V1.Modules.PayPal.DependencyInjection.Extensions
{
    public static class PayPalExtensions
    {
        public static IServiceCollection AddPayPal(this IServiceCollection services)
        {
            services.AddSingleton<IPayPalOptions, PayPalOptions>(sp =>
            {
                IConfiguration configuration = sp.GetRequiredService<IConfiguration>();

                PayPalOptions options = new();

                configuration.GetSection(nameof(PayPalOptions)).Bind(options);

                return options;
            });

            services.AddSingleton<IPayPalService, PayPalService>();
            return services;
        }
    }
}
