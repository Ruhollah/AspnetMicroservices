using AspnetRunBasics.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddTransient<LoggingDelegatingHandler>();

builder.Services.AddHttpClient<ICatalogService, CatalogService>(c =>
    c.BaseAddress = new Uri(builder.Configuration["ApiSettings:GatewayAddress"]))
    ;//.AddHttpMessageHandler<LoggingDelegatingHandler>()
    //.AddPolicyHandler(GetRetryPolicy())
    //.AddPolicyHandler(GetCircuitBreakerPolicy());

builder.Services.AddHttpClient<IBasketService, BasketService>(c =>
    c.BaseAddress = new Uri(builder.Configuration["ApiSettings:GatewayAddress"]))
    ;//.AddHttpMessageHandler<LoggingDelegatingHandler>()
     //.AddPolicyHandler(GetRetryPolicy())
     //.AddPolicyHandler(GetCircuitBreakerPolicy());

builder.Services.AddHttpClient<IOrderService, OrderService>(c =>
    c.BaseAddress = new Uri(builder.Configuration["ApiSettings:GatewayAddress"]))
    ;//.AddHttpMessageHandler<LoggingDelegatingHandler>()
     //.AddPolicyHandler(GetRetryPolicy())
     //.AddPolicyHandler(GetCircuitBreakerPolicy());





builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
