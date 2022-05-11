
using System.Reflection;
using GraphDemo;
using GraphDemo.Commands;
using GraphDemo.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Welcome to the Gremlin Test Console");

// create the host
var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(configuration =>
    {
        configuration.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true)
            .AddEnvironmentVariables();
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddTransient<IQuerySource, CosmosGremlinQuerySource>();
        services.AddTransient<CommandResolver>();
        services.AddHostedService<Application>();
    });
    
// run the host
host.Start();

Console.WriteLine("End");
