
using AivenEcommerce.V1.Application.Services;
using AivenEcommerce.V1.Application.Validators;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Services;
using AivenEcommerce.V1.Domain.Validators;
using AivenEcommerce.V1.Infrastructure.Options.Mongo;
using AivenEcommerce.V1.Infrastructure.Repositories;
using AivenEcommerce.V1.Modules.GitHub.DependencyInjection.Extensions;
using AivenEcommerce.V1.Modules.ImgBB.DependencyInjection.Extensions;
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


            services.AddGitHubClient();
            services.AddImgBb();

            services.AddHealthChecks(Configuration);

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<IProductVariantRepository, ProductVariantRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddScoped<IProductOverviewRepository, ProductOverviewRepository>();
            services.AddScoped<IProductBadgeRepository, ProductBadgeRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductImageService, ProductImageService>();
            services.AddScoped<IImageUploaderService, ImageUploaderService>();
            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            services.AddScoped<IProductOverviewService, ProductOverviewService>();
            services.AddScoped<IProductBadgeService, ProductBadgeService>();
            services.AddScoped<IProductVariantService, ProductVariantService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IBasketService, BasketService>();

            services.AddScoped<IProductValidator, ProductValidator>();
            services.AddScoped<IProductImageValidator, ProductImageValidator>();
            services.AddScoped<IProductCategoryValidator, ProductCategoryValidator>();
            services.AddScoped<IProductOverviewValidator, ProductOverviewValidator>();
            services.AddScoped<IProductVariantValidator, ProductVariantValidator>();
            services.AddScoped<ICustomerValidator, CustomerValidator>();
            services.AddScoped<IBasketValidator, BasketValidator>();

            services.AddForwardedHeaders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider, ILoggerFactory loggerFactory)
        {
            app.UseForwardedHeaders();
            app.UseRedirectToProxiedHttps();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseExceptionHandlingOperationResult(loggerFactory.CreateLogger("ExceptionHandler"));

            app.UseAllowAnyCors();

            app.UseSwaggerApiVersioning(provider);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseRedirectToProxiedHttps();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks();
                endpoints.MapControllers();
            });
        }
    }
}
