using AivenEcommerce.V1.Application.Extensions;
using AivenEcommerce.V1.Domain.Shared.Common;
using AivenEcommerce.V1.Infrastructure.Extensions.Factories;
using AivenEcommerce.V1.Infrastructure.Repositories.Base;

using Microsoft.Extensions.DependencyInjection;

using System.Linq;

namespace AivenEcommerce.V1.WebApi.Startup
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddInterfaceApplicationServices();
            services.AddPaymentProviderFactory();
            return services;
        }


        private static IServiceCollection AddInterfaceApplicationServices(this IServiceCollection services)
        {
            var serviceInterfaceType = typeof(IService);
            var singletonServiceInterfaceType = typeof(ISingletonService);
            var scopedServiceInterfaceType = typeof(IScopedService);
            var githubRepositoryType = typeof(GitHubRepository<,>);
            var stringExtensionsType = typeof(StringExtensions);

            var types = githubRepositoryType
                .Assembly
                .GetExportedTypes()

                .Concat(stringExtensionsType
                .Assembly
                .GetExportedTypes())

                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Service = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .Where(t => t.Service != null);

            foreach (var type in types)
            {
                if (serviceInterfaceType.IsAssignableFrom(type.Service))
                {
                    services.AddTransient(type.Service, type.Implementation);
                }
                else if (singletonServiceInterfaceType.IsAssignableFrom(type.Service))
                {
                    services.AddSingleton(type.Service, type.Implementation);
                }
                else if (scopedServiceInterfaceType.IsAssignableFrom(type.Service))
                {
                    services.AddScoped(type.Service, type.Implementation);
                }
            }
            return services;
        }
    }
}
