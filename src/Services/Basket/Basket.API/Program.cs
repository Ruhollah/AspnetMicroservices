using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Basket.API.Repositories.Interfaces;
using Discount.Grpc.Protos;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Redis Configuration
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});

// General Configuration
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddAutoMapper(typeof(Program));

// Grpc Configuration
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>
            (o => o.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]));
builder.Services.AddScoped<DiscountGrpcService>();

// MassTransit-RabbitMQ Configuration
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        //cfg.UseHealthCheck(ctx);
        //cfg.ConfigureEndpoints(ctx);
    });
});
//builder.Services.AddMassTransitHostedService();
//builder.Services.AddHostedService<Worker>();


//https://github.com/MassTransit/Sample-GettingStarted/blob/master/GettingStarted/Program.cs
//builder.Services.AddMassTransit(x =>
//{
//    x.AddConsumer<MessageConsumer>();

//    x.UsingRabbitMq((context, cfg) =>
//    {
//        cfg.ConfigureEndpoints(context);
//    });
//});
//builder.Services.AddHostedService<Worker>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
