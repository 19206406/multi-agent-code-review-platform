using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Messaging.Contracts.Messages;
using System.Text.Json;
using WebhookGateway.Common.Models;
using WebhookGateway.Common.Validation;
using WebhookGateway.Infrastructure.Kafka.Producer;
using WebhookGateway.Infrastructure.Redis.Idempotency;

namespace WebhookGateway.Features.WebhooksGithub
{
    public class WebhooksGithubCommandHandler : ICommandHandler<WebhooksGitHubCommand, WebhooksGithubResponse>
    {
        private readonly IHmacSignatureValidator _validator;
        private readonly IRedisIdempotencyService _redis;
        private readonly PrEventProducer _producer;

        public WebhooksGithubCommandHandler(IHmacSignatureValidator validator, IRedisIdempotencyService redis, PrEventProducer producer)
        {
            _validator = validator;
            _redis = redis;
            _producer = producer;
        }

        public async Task<WebhooksGithubResponse> Handle(WebhooksGitHubCommand command, CancellationToken cancellationToken)
        {
            if (!_validator.IsValid(command.RawPayload, command.Signature))
                throw new UnauthorizedException("Unauthorized the signature does not match");

            if (command.EventType == "ping")
                return new WebhooksGithubResponse(true);

            if (command.EventType != "pull_request")
                return new WebhooksGithubResponse(true);


            // idempotency 
            if (await _redis.IsAlreadyProcessedAsync(command.DeliveryId))
                return new WebhooksGithubResponse(true);

            var githubEvent = JsonSerializer.Deserialize<GithubPrPayload>(command.RawPayload, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            })!;

            // I verify that the stock type is one of these
            if (githubEvent.Action is not ("opened" or "synchronize" or "reopened"))
                return new WebhooksGithubResponse(true);

            var messageEvent = Convert(githubEvent, command);

            // send message kafka
            await _producer.PublicAsync(messageEvent, cancellationToken); 

            // idempotency 
            await _redis.MarkAsProcessedAsync(command.DeliveryId);

            return new WebhooksGithubResponse(true); 
        }

        private PrEventMessage Convert(GithubPrPayload payload, WebhooksGitHubCommand command)
        {
            var messageEvent = new PrEventMessage(
                Guid.CreateVersion7().ToString(),
                DateTime.UtcNow,
                command.DeliveryId,
                payload.Action,
                new Repository(
                    payload.Repository.FullName,
                    payload.Repository.DefaultBranch,
                    payload.Repository.Language,
                    payload.Repository.CloneUrl,
                    payload.Repository.HtmlUrl),
                new PullRequest(
                    payload.PullRequest.Number,
                    payload.PullRequest.Title,
                    payload.PullRequest.State,
                    payload.PullRequest.Url,
                    payload.PullRequest.HtmlUrl,
                    new PullRequestUser(payload.PullRequest.User.Login),
                    new PullRequestHead(payload.PullRequest.Head.Sha, payload.PullRequest.Head.Ref),
                    new PullRequestBase(payload.PullRequest.Base.Sha, payload.PullRequest.Base.Ref),
                    payload.PullRequest.DiffUrl,
                    payload.PullRequest.CreatedAt,
                    payload.PullRequest.UpdatedAt)
                );

            return messageEvent; 
        }
    }
}
