
using AivenEcommerce.V1.Infrastructure.Options;

using Microsoft.Extensions.DependencyInjection;

using Octokit;

namespace AivenEcommerce.V1.WebApi.Extensions
{
    public static class GitHubExtensions
    {
        public static IServiceCollection AddGitHubClient(this IServiceCollection services)
        { 
            services.AddSingleton<IGitHubClient, GitHubClient>(sp => 
            {
                IGitHubOptions options = sp.GetRequiredService<IGitHubOptions>();

                GitHubClient githubClient = new GitHubClient(new ProductHeaderValue(nameof(AivenEcommerce)));

                Credentials basicAuth = new Credentials(options.Token);

                githubClient.Credentials = basicAuth;

                return githubClient;
            });

            return services;
        }
    }
}
