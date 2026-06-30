using BuildingBlocks.CQRS;

namespace WebhookGateway.Features.WebhooksGithub
{
    public record WebhooksGitHubCommand(string DeliveryId, string Signature, string EventType, string RawPayload) 
        : ICommand<WebhooksGithubResponse>; 
}
