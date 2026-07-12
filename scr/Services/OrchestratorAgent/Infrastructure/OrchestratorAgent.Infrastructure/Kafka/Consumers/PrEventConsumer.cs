using BuildingBlocks.Messaging.Contracts.Messages;
using BuildingBlocks.Messaging.Kafka.Constants;
using BuildingBlocks.Messaging.Kafka.Consumers;
using BuildingBlocks.Messaging.Kafka.Options;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace OrchestratorAgent.Infrastructure.Kafka.Consumers
{
    public class PrEventConsumer : KafkaConsumerBase<string, PrEventMessage>
    {
        private readonly ILogger<PrEventConsumer> _logger;

        protected override string TopicName => "pr.events.raw";

        public PrEventConsumer(
            IOptions<KafkaOptions> options, ILogger<PrEventConsumer> logger) : base(options, logger)
        {
            _logger = logger;
        }

        protected override async Task HandleMessageAsync(PrEventMessage message, Headers headers, CancellationToken cancellationToken)
        {
            var correlationId = headers.TryGetLastBytes(KafkaHeaderKeys.CorrelationId, out var bytes)
                ? System.Text.Encoding.UTF8.GetString(bytes)
                : message.EventId;

            _logger.LogInformation(
                "Processing PR event #{PrNumber} from the {Repository} repository", 
                message.PullRequest.Number, 
                message.Repository.FullName
            ); 
        }
    }
}
