using BuildingBlocks.Messaging.Contracts.Messages;
using BuildingBlocks.Messaging.Kafka.Constants;
using BuildingBlocks.Messaging.Kafka.Consumers;
using BuildingBlocks.Messaging.Kafka.Options;
using Confluent.Kafka;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrchestratorAgent.Application.Contracts.Persistence;
using OrchestratorAgent.Application.Features.Commands.CreatePipelineRun;
using OrchestratorAgent.Domain.Entities;

namespace OrchestratorAgent.Infrastructure.Kafka.Consumers
{
    public class PrEventConsumer : KafkaConsumerBase<string, PrEventMessage>
    {
        private readonly ILogger<PrEventConsumer> _logger;
        private readonly IPipelineRunRepository _pipelineRunRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        protected override string TopicName => "pr.events.raw";

        public PrEventConsumer(
            IOptions<KafkaOptions> options, ILogger<PrEventConsumer> logger, 
            IPipelineRunRepository pipelineRunRepository, IUnitOfWork unitOfWork,
            IMediator mediator) : base(options, logger)
        {
            _logger = logger;
            _pipelineRunRepository = pipelineRunRepository;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
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

            var command = new CreatePipelineRunCommand(
                correlationId, message.Repository.FullName, message.PullRequest.Number, message.PullRequest.Title,
                message.PullRequest.Author, message.PullRequest.Head.Sha, message.PullRequest.Base.Sha,
                message.PullRequest.HeadBranch, message.PullRequest.BaseBranch, message.GitHubDeliveryId);

            var created = await _mediator.Send(command);
            
            // TODO: Assess how successes and errors are being handled within the consumer base.
            _logger.LogInformation(
                "PR event {EventId} processed successfully. PipelineRunId: {PipelineRunId}",
                message.EventId,
                created.Id);
        }
    }
}
