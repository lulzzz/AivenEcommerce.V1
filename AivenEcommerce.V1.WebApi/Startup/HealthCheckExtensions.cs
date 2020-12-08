using AivenEcommerce.V1.Infrastructure.Options.ClientConfig;
using AivenEcommerce.V1.Infrastructure.Options.Mongo;
using AivenEcommerce.V1.Modules.GitHub.DependencyInjection.HealthChecks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

using MongoDB.HealthCheck;

using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.WebApi.Startup
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            IMongoProductOptions mongoProductOptions = new MongoProductOptions();
            IMongoOrderOptions mongoOrderOptions = new MongoOrderOptions();
            IMongoSaleOptions mongoSaleOptions = new MongoSaleOptions();

            configuration.GetSection(nameof(MongoProductOptions)).Bind(mongoProductOptions);
            configuration.GetSection(nameof(MongoOrderOptions)).Bind(mongoOrderOptions);
            configuration.GetSection(nameof(MongoSaleOptions)).Bind(mongoSaleOptions);

            services.AddHealthChecks()
                .AddMongoHealthCheck("mongodbproducts", mongoProductOptions.ConnectionString)
                .AddMongoHealthCheck("mongodborders", mongoOrderOptions.ConnectionString)
                .AddMongoHealthCheck("mongodbsales", mongoSaleOptions.ConnectionString)
                .AddCheck<GitHubHealthCheck>("githubapi");

            return services;
        }

        public static IEndpointConventionBuilder MapHealthChecks(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapHealthChecks("/health", new HealthCheckOptions()
            {
                AllowCachingResponses = false,
                ResponseWriter = WriteResponse
            });
        }

        private static Task WriteResponse(HttpContext context, HealthReport result)
        {
            IClientConfigOptions clientConfigOptions = context.RequestServices.GetRequiredService<IClientConfigOptions>();
            context.Response.ContentType = "application/json; charset=utf-8";

            var options = new JsonWriterOptions
            {
                Indented = true
            };

            using MemoryStream stream = new();
            using (Utf8JsonWriter writer = new(stream, options))
            {
                writer.WriteStartObject();
                writer.WriteString("buildVersion", clientConfigOptions.BuildVersion);
                writer.WriteString("status", result.Status.ToString());
                writer.WriteStartObject("results");
                foreach (var entry in result.Entries)
                {
                    writer.WriteStartObject(entry.Key);
                    writer.WriteString("status", entry.Value.Status.ToString());
                    writer.WriteString("description", entry.Value.Description);
                    writer.WriteStartObject("data");
                    foreach (var item in entry.Value.Data)
                    {
                        writer.WritePropertyName(item.Key);
                        JsonSerializer.Serialize(
                            writer, item.Value, item.Value?.GetType() ??
                            typeof(object));
                    }
                    writer.WriteEndObject();
                    writer.WriteEndObject();
                }
                writer.WriteEndObject();
                writer.WriteEndObject();
            }

            var json = Encoding.UTF8.GetString(stream.ToArray());

            return context.Response.WriteAsync(json);
        }
    }
}
