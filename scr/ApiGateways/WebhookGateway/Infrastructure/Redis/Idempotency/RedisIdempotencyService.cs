using StackExchange.Redis;

namespace WebhookGateway.Infrastructure.Redis.Idempotency
{
    public class RedisIdempotencyService : IRedisIdempotencyService
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly int Ttlhours = 24; 

        public RedisIdempotencyService(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task<bool> IsAlreadyProcessedAsync(string deliveryId)
        {
            var db = _redis.GetDatabase();
            // key - I don't need a value because I just want to check if a key exists 
            return await db.KeyExistsAsync($"idempotency:webhook:{deliveryId}"); 
        }

        public async Task MarkAsProcessedAsync(string deliveryId)
        {
            var db = _redis.GetDatabase();
            // key - value - time 
            await db.StringSetAsync($"idempotency:webhook:{deliveryId}", "processed", TimeSpan.FromHours(Ttlhours)); 
        }
    }
}
