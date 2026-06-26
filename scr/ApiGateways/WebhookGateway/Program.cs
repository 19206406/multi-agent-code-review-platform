using FastEndpoints;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using WebhookGateway.Infrastructure.Kafka;
using WebhookGateway.Infrastructure.Kafka.Service;
using WebhookGateway.Infrastructure.Redis;
using WebhookGateway.Infrastructure.Redis.Services;

var builder = WebApplication.CreateBuilder(args);

// FastEndpoints 
builder.Services.AddFastEndpoints();

// Redis 
builder.Services.AddRedisCache(builder.Configuration);
builder.Services.AddSingleton<ICacheService, CacheService>();

// Kafka 
builder.Services.AddKafkaProducer(builder.Configuration);
// handler of each slice 

// Health-check 
builder.Services.AddHealthChecks()
    .AddRedis(
    redisConnectionString: builder.Configuration["Redis:ConnectionString"]!,
    name: "redis:cache",
    tags: ["cache", "infrastructure"])
    .AddCheck<KafkaHealthCheck>(
    name: "kafka:broker",
    tags: ["messaging", "infrastructure"]); 

var app = builder.Build();

// FastEndpoints Middleware 
app.UseFastEndpoints();

// Health-check Middleware 
app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = _=> true, 
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
}); 

app.Run();
