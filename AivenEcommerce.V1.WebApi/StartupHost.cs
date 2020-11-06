
using AivenEcommerce.V1.Application.Services;
using AivenEcommerce.V1.Application.Validators;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Services;
using AivenEcommerce.V1.Domain.Validators;
using AivenEcommerce.V1.Infrastructure.Options;
using AivenEcommerce.V1.Infrastructure.Options.Mongo;
using AivenEcommerce.V1.Infrastructure.Repositories;
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
            services.AddSwaggerApiVersioning();


            services.AddOptions<IMongoProductOptions, MongoProductOptions>(Configuration);
            services.AddOptions<IMongoProductDetailOptions, MongoProductDetailOptions>(Configuration);
            services.AddOptions<IMongoOrderOptions, MongoOrderOptions>(Configuration);
            services.AddOptions<IMongoDeliveryOptions, MongoDeliveryOptions>(Configuration);
            services.AddOptions<IMongoBasketOptions, MongoBasketOptions>(Configuration);
            services.AddOptions<IMongoBasketItemOptions, MongoBasketItemOptions>(Configuration);
            services.AddOptions<IMongoBasketItemDetailOptions, MongoBasketItemDetailOptions>(Configuration);
            services.AddOptions<IGitHubOptions, GitHubOptions>(Configuration);


            services.AddGitHubClient();

            services.AddHealthChecks(Configuration);

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductDetailRepository, ProductDetailRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IDeliveryRepository, DeliveryRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBasketItemRepository, BasketItemRepository>();
            services.AddScoped<IBasketItemDetailRepository, BasketItemDetailRepository>();

            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IProductValidator, ProductValidator>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseExceptionHandlingOperationResult(loggerFactory.CreateLogger("ExceptionHandler"));

            app.UseSwaggerApiVersioning(provider);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks();
                endpoints.MapControllers();
            });
        }
    }
}
