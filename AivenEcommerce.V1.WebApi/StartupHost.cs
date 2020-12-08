using AivenEcommerce.V1.Infrastructure.Options.ClientConfig;
using AivenEcommerce.V1.Infrastructure.Options.Mongo;
using AivenEcommerce.V1.Infrastructure.Options.PaymentProvider;
using AivenEcommerce.V1.Modules.GitHub.DependencyInjection.Extensions;
using AivenEcommerce.V1.Modules.ImgBB.DependencyInjection.Extensions;
using AivenEcommerce.V1.Modules.PayPal.DependencyInjection.Extensions;
using AivenEcommerce.V1.WebApi.Startup;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AivenEcommerce.V1.WebApi
{
    public class StartupHost
    {
        public StartupHost(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerApiVersioning()


            .AddOptions<IClientConfigOptions, ClientConfigOptions>(Configuration)
            .AddOptions<IPaymentProviderOptions, PaymentProviderOptions>(Configuration)
            .AddOptions<IMongoProductOptions, MongoProductOptions>(Configuration)
            .AddOptions<IMongoSaleOptions, MongoSaleOptions>(Configuration)
            .AddOptions<IMongoOrderOptions, MongoOrderOptions>(Configuration)


            .AddGitHubClient()
            .AddImgBb()
            .AddPayPal()

            .AddHealthChecks(Configuration)

            .AddApplicationServices()

            .AddForwardedHeaders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider, ILoggerFactory loggerFactory)
        {
            app.UseForwardedHeaders()
            .UseRedirectToProxiedHttps();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseExceptionHandlingOperationResult(loggerFactory.CreateLogger("ExceptionHandler"))

            .UseAllowAnyCors()

            .UseSwaggerApiVersioning(provider)

            .UseHttpsRedirection()

            .UseRouting()

            .UseAuthorization()

            .UseRedirectToProxiedHttps()

            .UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks();
                endpoints.MapControllers();
            });
        }
    }
}
