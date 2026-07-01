using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Messaging.Contracts.Messages;
using System.Text.Json;
using WebhookGateway.Common.Models;
using WebhookGateway.Common.Validation;
using WebhookGateway.Infrastructure.Redis.Idempotency;

namespace WebhookGateway.Features.WebhooksGithub
{
    public class WebhooksGithubCommandHandler : ICommandHandler<WebhooksGitHubCommand, WebhooksGithubResponse>
    {
        private readonly IHmacSignatureValidator _validator;
        //private readonly IRedisIdempotencyService _redis;

        public WebhooksGithubCommandHandler(IHmacSignatureValidator validator)
        {
            _validator = validator;
            //_redis = redis;
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
            //if (await _redis.IsAlreadyProcessedAsync(command.DeliveryId))
            //    return new WebhooksGithubResponse(true);

            var githubEvent = JsonSerializer.Deserialize<GithubPrPayload>(command.RawPayload)!;

            // I verify that the stock type is one of these
            if (githubEvent.Action is not ("opened" or "synchronize" or "reopened"))
                return new WebhooksGithubResponse(true);

            //var messageEvent = new PrEventMessage(Guid.CreateVersion7(), githubEvent.)

            throw new NotImplementedException();
        }
    }
}
