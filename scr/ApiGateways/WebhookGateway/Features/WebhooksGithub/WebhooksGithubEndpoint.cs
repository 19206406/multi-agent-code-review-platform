using FastEndpoints;

namespace WebhookGateway.Features.WebhooksGithub
{
    public record WebhooksGitHubRequest(int id); 
    public class WebhooksGithubEndpoint : Endpoint<WebhooksGitHubRequest>
    {
        public override void Configure()
        {
            Get("api/webhooks");
            AllowAnonymous(); 
        }

        public override Task HandleAsync(WebhooksGitHubRequest req, CancellationToken ct)
        {
            return base.HandleAsync(req, ct);
        }
    }
}
