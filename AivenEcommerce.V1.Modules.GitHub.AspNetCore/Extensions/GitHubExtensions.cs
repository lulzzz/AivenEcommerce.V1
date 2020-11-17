using AivenEcommerce.V1.Modules.GitHub.Options;
using AivenEcommerce.V1.Modules.GitHub.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Octokit;

namespace AivenEcommerce.V1.WebApi.Startup
{
    public static class GitHubExtensions
    {
        public static IServiceCollection AddGitHubClient(this IServiceCollection services)
        {
            services.AddSingleton<IGitHubOptions, GitHubOptions>(sp =>
            {
                IConfiguration configuration = sp.GetRequiredService<IConfiguration>();

                GitHubOptions options = new();

                configuration.GetSection(nameof(GitHubOptions)).Bind(options);

                return options;
            });

            services.AddSingleton<IGitHubClient, GitHubClient>(sp =>
            {
                IGitHubOptions options = sp.GetRequiredService<IGitHubOptions>();

                GitHubClient githubClient = new(new ProductHeaderValue(nameof(AivenEcommerce)));

                Credentials basicAuth = new(options.Token);

                githubClient.Credentials = basicAuth;

                return githubClient;
            });

            services.AddSingleton<IGitHubService, GitHubService>();

            return services;
        }
    }
}
