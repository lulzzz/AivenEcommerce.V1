using Microsoft.Extensions.Diagnostics.HealthChecks;

using Octokit;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Modules.GitHub.DependencyInjection.HealthChecks
{
    public class GitHubHealthCheck : IHealthCheck
    {
        private readonly IGitHubClient _githubClient;

        public GitHubHealthCheck(IGitHubClient githubClient)
        {
            _githubClient = githubClient ?? throw new ArgumentNullException(nameof(githubClient));
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            await _githubClient.User.Current();
            return HealthCheckResult.Healthy();
        }
    }
}
