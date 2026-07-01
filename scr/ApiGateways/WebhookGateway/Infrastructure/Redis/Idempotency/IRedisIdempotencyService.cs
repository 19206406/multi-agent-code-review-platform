namespace WebhookGateway.Infrastructure.Redis.Idempotency
{
    public interface IRedisIdempotencyService
    {
        Task<bool> IsAlreadyProcessedAsync(string deliveryId);
        Task MarkAsProcessedAsync(string deliveryId); 
    }
}
