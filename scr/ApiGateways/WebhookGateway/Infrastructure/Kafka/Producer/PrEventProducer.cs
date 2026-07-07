using BuildingBlocks.Messaging.Contracts.Messages;
using BuildingBlocks.Messaging.Kafka.Constants;
using BuildingBlocks.Messaging.Kafka.Options;
using BuildingBlocks.Messaging.Kafka.Producers;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System.Text;

namespace WebhookGateway.Infrastructure.Kafka.Producer
{
    public class PrEventProducer : KafkaProducerBase<string, PrEventMessage> 
    {
        private const string Topic = "pr.events.raw";

        public PrEventProducer
            (IOptions<KafkaOptions> options, ILogger<PrEventProducer> logger) : base(options, logger)
        {
        }

        public async Task PublicAsync(PrEventMessage message, CancellationToken cancellationToken = default)
        {
            // partition key - by repository 
            var partitionKey = message.Repository.FullName;

            var headers = new Headers
            {
                { KafkaHeaderKeys.CorrelationId, Encoding.UTF8.GetBytes(message.EventId) },
                { KafkaHeaderKeys.ReceivedAt, Encoding.UTF8.GetBytes(
                    DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString()) }
            };

            await ProduceAsync(Topic, partitionKey, message, headers, cancellationToken); 
        }
    }
}
