
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Infrastructure.Options;
using AivenEcommerce.V1.Infrastructure.Options.Mongo;
using AivenEcommerce.V1.Infrastructure.Repositories;
using AivenEcommerce.V1.WebApi.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AivenEcommerce.V1.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AivenEcommerce.V1", Version = "v1" });
            });

            services.AddOptions<IMongoProductOptions, MongoProductOptions>(Configuration);
            services.AddOptions<IMongoProductDetailOptions, MongoProductDetailOptions>(Configuration);
            services.AddOptions<IMongoOrderOptions, MongoOrderOptions>(Configuration);
            services.AddOptions<IMongoDeliveryOptions, MongoDeliveryOptions>(Configuration);
            services.AddOptions<IMongoBasketOptions, MongoBasketOptions>(Configuration);
            services.AddOptions<IMongoBasketItemOptions, MongoBasketItemOptions>(Configuration);
            services.AddOptions<IMongoBasketItemDetailOptions, MongoBasketItemDetailOptions>(Configuration);
            services.AddOptions<IGitHubOptions, GitHubOptions>(Configuration);


            services.AddGitHubClient();


            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductDetailRepository, ProductDetailRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IDeliveryRepository, DeliveryRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBasketItemRepository, BasketItemRepository>();
            services.AddScoped<IBasketItemDetailRepository, BasketItemDetailRepository>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AivenEcommerce.V1 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
