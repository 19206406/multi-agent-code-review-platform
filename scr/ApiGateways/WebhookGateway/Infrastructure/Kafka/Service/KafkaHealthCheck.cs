using Confluent.Kafka;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using WebhookGateway.Infrastructure.Kafka.Services;

namespace WebhookGateway.Infrastructure.Kafka.Service
{
    public sealed class KafkaHealthCheck : IHealthCheck
    {
        private readonly KafkaOptions _options;

        public KafkaHealthCheck(IOptions<KafkaOptions> options)
        {
            _options = options.Value;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var config = new AdminClientConfig
                {
                    BootstrapServers = _options.BootstrapServers,
                    SocketTimeoutMs = 3000,
                    MetadataMaxAgeMs = 3000
                };

                using var adminClient = new AdminClientBuilder(config).Build();

                var metadata = await Task.Run(() => adminClient.GetMetadata(timeout: TimeSpan.FromSeconds(3)), cancellationToken);

                var brokerCount = metadata.Brokers.Count; 

                return HealthCheckResult.Healthy(
                description: $"Kafka reachable. Brokers: {brokerCount}.",
                data: new Dictionary<string, object>
                {
                    ["brokers"] = brokerCount,
                    ["bootstrap_servers"] = _options.BootstrapServers
                }); 
            }
            catch(Exception ex)
            {
                return HealthCheckResult.Unhealthy(
                description: "Kafka unreachable.",
                exception: ex,
                data: new Dictionary<string, object>
                {
                    ["bootstrap_servers"] = _options.BootstrapServers
                });
            }
        }
    }
}
