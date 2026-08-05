using BuildingBlocks.Messaging.Kafka.Extensions;
using BuildingBlocks.Middlewares;
using FastEndpoints;
using FastEndpoints.Swagger;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Scalar.AspNetCore;
using System.Reflection;
using WebhookGateway.Common.Options;
using WebhookGateway.Common.Validation;
using WebhookGateway.Infrastructure.Kafka;
using WebhookGateway.Infrastructure.Kafka.Producer;
using WebhookGateway.Infrastructure.Kafka.Service;
using WebhookGateway.Infrastructure.Redis;
using WebhookGateway.Infrastructure.Redis.Idempotency;

var builder = WebApplication.CreateBuilder(args);


// TODO: create partitions, topics and brokers_count etc... 

// FastEndpoints 
builder.Services.AddFastEndpoints();

// FastEndpoints swagger
builder.Services.SwaggerDocument(); 
//builder.Services.SwaggerDocument(options =>
//{
//    options.DocumentSettings = s =>
//    {
//        s.Title = "webhook-service-api";
//        s.Version = "v1";
//    };
//    options.AutoTagPathSegmentIndex = 0;
//});

// options apppsettings.json 
builder.Services.Configure<WebhookOptions>(
    builder.Configuration.GetSection(WebhookOptions.SectionName)); 

// MediatR 
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

//Redis
builder.Services.AddRedisCache(builder.Configuration);
builder.Services.AddSingleton<IRedisIdempotencyService, RedisIdempotencyService>();

// Kafka 
builder.Services.AddKafkaProducer(builder.Configuration);
// handler of each slice 

// Kafka Messaging 
builder.Services.AddKafkaInfratructure(builder.Configuration);
builder.Services.AddSingleton<PrEventProducer>(); 

// MACSHA256 Validation 
builder.Services.AddScoped<IHmacSignatureValidator, HmacSignatureValidator>();

// Health-check 
builder.Services.AddHealthChecks()
    .AddRedis(
    redisConnectionString: builder.Configuration["Redis:ConnectionString"]!,
    name: "redis:cache",
    tags: ["cache", "infrastructure"])
    .AddCheck<KafkaHealthCheck>(
    name: "kafka:broker",
    tags: ["messaging", "infrastructure"]);

// Activar el tunel de ngrok con github. --- comando: ngrok start webhook-gateway

var app = builder.Build();

// FastEndpoints Middleware 
app.UseFastEndpoints();
app.UseSwaggerGen(options =>
{
    options.Path = "/openapi/{documentName}.json";
});
app.MapScalarApiReference();

app.UseSwaggerUi();

// custom exceptions
//app.UseExceptionHandler(); 
app.UseCustomExceptionHandler(); 

// Health-check Middleware 
app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = _=> true, 
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
}); 

app.Run();
