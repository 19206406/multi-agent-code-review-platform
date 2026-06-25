using FastEndpoints;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using WebhookGateway.Infrastructure.Redis;
using WebhookGateway.Infrastructure.Redis.Services;

var builder = WebApplication.CreateBuilder(args);

// FastEndpoints 
builder.Services.AddFastEndpoints();

// Redis 
builder.Services.AddRedisCache(builder.Configuration);
builder.Services.AddSingleton<ICacheService, CacheService>();

// Health-check 
builder.Services.AddHealthChecks()
    .AddRedis(
    redisConnectionString: builder.Configuration["Redis:ConnectionString"]!,
    name: "redis:cache",
    tags: ["cache", "infrastructure"]); 

var app = builder.Build();

// FastEndpoints Middleware 
app.UseFastEndpoints();

// Health-check Middleware 
app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _=> true, 
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
}); 

app.UseAuthorization();

app.Run();
