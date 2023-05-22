using System.Text.Json.Serialization;
using Masstransit.SagaPoc.ClientB.Application;
using Masstransit.SagaPoc.ClientB.Infrastructure;
using Serilog;

const int EXIT_FAILURE = 1;
const int EXIT_SUCCESS = 0;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddEnvironmentVariables("CONFIG_");

builder.Host.UseSerilog((ctx, lc) => lc
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.WithProperty(nameof(ServiceConstants.ServiceName), ServiceConstants.ServiceName)
    .Enrich.WithProperty(nameof(ServiceConstants.ServiceVersion), ServiceConstants.ServiceVersion));

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.WriteIndented = true;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

try
{
    Log.Information("Starting host...");

    var app = builder.Build();

    app.UseRouting();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

    await app.RunAsync();

    return EXIT_SUCCESS;
}
catch (Exception exception)
{
    Log.Fatal(exception, "Host terminated unexpectedly");

    return EXIT_FAILURE;
}
finally
{
    Log.CloseAndFlush();
}
