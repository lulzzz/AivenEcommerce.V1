
using AivenEcommerce.V1.WebApi;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;


Host.CreateDefaultBuilder()
    .ConfigureHostConfiguration(configurationBuilder =>
    {
        configurationBuilder.AddJsonFile("clientconfig.json", optional: false, reloadOnChange: true);
        configurationBuilder.AddJsonFile("clientconfig.Development.json", optional: false, reloadOnChange: true);
    })
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<StartupHost>();
    })
    .Build().Run();
