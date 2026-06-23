using FastEndpoints;
using WebhookGateway.Infrastructure.Redis;
using WebhookGateway.Infrastructure.Redis.Services;

var builder = WebApplication.CreateBuilder(args);

// FastEndpoints 
builder.Services.AddFastEndpoints();

// Redis 
builder.Services.AddRedisCache(builder.Configuration);
builder.Services.AddSingleton<ICacheService, CacheService>(); 

var app = builder.Build();

// FastEndpoints Middleware 
app.UseFastEndpoints(); 

app.UseAuthorization();

app.Run();
