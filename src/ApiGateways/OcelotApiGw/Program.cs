using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true);
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"))
    .AddConsole()
    .AddDebug();

builder.Services.AddOcelot()
    .AddCacheManager(settings => settings.WithDictionaryHandle());


var app = builder.Build();


app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/",
        async context => { await context.Response.WriteAsync("Ocelot API Gateway"); });
});

//app.MapGet("/", () => "Hello World!");

await app.UseOcelot();

app.Run();
