
using AivenEcommerce.V1.WebApi;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


Host.CreateDefaultBuilder()
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<StartupHost>();
    }).Build().Run();
