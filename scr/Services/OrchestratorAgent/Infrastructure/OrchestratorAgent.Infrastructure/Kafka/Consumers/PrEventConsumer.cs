using BuildingBlocks.Messaging.Contracts.Messages;
using BuildingBlocks.Messaging.Kafka.Constants;
using BuildingBlocks.Messaging.Kafka.Consumers;
using BuildingBlocks.Messaging.Kafka.Options;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrchestratorAgent.Application.Contracts.Persistence;
using OrchestratorAgent.Domain.Entities;

namespace OrchestratorAgent.Infrastructure.Kafka.Consumers
{
    public class PrEventConsumer : KafkaConsumerBase<string, PrEventMessage>
    {
        private readonly ILogger<PrEventConsumer> _logger;
        private readonly IPipelineRunRepository _pipelineRunRepository;

        protected override string TopicName => "pr.events.raw";

        public PrEventConsumer(
            IOptions<KafkaOptions> options, ILogger<PrEventConsumer> logger, IPipelineRunRepository pipelineRunRepository) : base(options, logger)
        {
            _logger = logger;
            _pipelineRunRepository = pipelineRunRepository;
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

            var newPipelineRun = new PipelineRun
            {
                CorrelationId = Guid.Parse(correlationId),
                Status = 0,
                RepositoryFullName = message.Repository.FullName,
                PrNumber = message.PullRequest.Number,
                PrTitle = message.PullRequest.Title,
                PrAuthor = message.PullRequest.Author,
                HeadSha = message.PullRequest.Head.Sha,
                BaseSha = message.PullRequest.Base.Sha,
                HeadBranch = message.PullRequest.HeadBranch,
                BaseBranch = message.PullRequest.BaseBranch,
                GithubDeliveryId = message.GitHubDeliveryId,
                RetryCount = 0,
                StartedAt = DateTimeOffset.UtcNow,
            };

            await _pipelineRunRepository.CratePipelineRunAsync(newPipelineRun); 
        }
    }
}
