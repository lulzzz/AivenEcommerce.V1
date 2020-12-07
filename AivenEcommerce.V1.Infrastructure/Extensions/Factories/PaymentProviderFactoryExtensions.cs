using AivenEcommerce.V1.Application.Factories.PaymentProviders;
using AivenEcommerce.V1.Domain.Factories.PaymentProviders;
using AivenEcommerce.V1.Domain.Shared.Enums;
using AivenEcommerce.V1.Modules.PayPal.Services;

using Microsoft.Extensions.DependencyInjection;

namespace AivenEcommerce.V1.Infrastructure.Extensions.Factories
{
    public static class PaymentProviderFactoryExtensions
    {
        public static IServiceCollection AddPaymentProviderFactory(this IServiceCollection services)
        {
            services.AddScoped<IPaymentProviderFactory>(sp =>
            {
                PaymentProviderFactory factory = new();
                IPayPalService payPalService = sp.GetRequiredService<IPayPalService>();

                factory.RegisterPaymentProvider(PaymentProvider.PayPal, () => new PayPalPaymentProvider(payPalService));

                return factory;
            });

            return services;
        }
    }
}
