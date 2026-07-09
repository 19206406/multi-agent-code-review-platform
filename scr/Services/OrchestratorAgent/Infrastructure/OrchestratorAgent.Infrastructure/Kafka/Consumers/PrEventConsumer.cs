using BuildingBlocks.Messaging.Contracts.Messages;
using BuildingBlocks.Messaging.Kafka.Consumers;
using BuildingBlocks.Messaging.Kafka.Options;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace OrchestratorAgent.Infrastructure.Kafka.Consumers
{
    public class PrEventConsumer : KafkaConsumerBase<string, PrEventMessage>
    {

        protected override string TopicName => "pr.events.raw";

        public PrEventConsumer(
            IOptions<KafkaOptions> options, ILogger<PrEventConsumer> logger) : base(options, logger)
        {
        }

        protected override Task HandleMessageAsync(PrEventMessage message, Headers headers, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
