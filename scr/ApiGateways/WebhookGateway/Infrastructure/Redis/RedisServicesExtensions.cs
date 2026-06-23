using StackExchange.Redis;

namespace WebhookGateway.Infrastructure.Redis
{
    public static class RedisServicesExtensions
    {
        public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["Redis:ConnectionString"]!;

            services.AddSingleton<IConnectionMultiplexer>(
                ConnectionMultiplexer.Connect(connectionString));

            // Enable distributed caching 
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = connectionString;
                options.InstanceName = "WebhookGateway"; 
            });

            return services; 
        }
    }
}
