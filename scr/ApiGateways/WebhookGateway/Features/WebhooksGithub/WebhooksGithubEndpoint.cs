using FastEndpoints;
using MediatR;
using System.Text.Json;

namespace WebhookGateway.Features.WebhooksGithub
{
    public class WebhooksGithubEndpoint : EndpointWithoutRequest
    {
        private readonly IMediator _mediator;

        public WebhooksGithubEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Post("/webhooks/github");
            AllowAnonymous(); 
        }

        public override async Task HandleAsync(CancellationToken ct)
        {

            string deliveryId = HttpContext.Request.Headers["X-GitHub-Delivery"].ToString();
            string signature = HttpContext.Request.Headers["X-Hub-Signature-256"].ToString();
            string eventType = HttpContext.Request.Headers["X-GitHub-Event"].ToString();

            // webhook body 
            string payload = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync(ct);

            var json = JsonSerializer.Deserialize<JsonElement>(payload);

            var command = new WebhooksGitHubCommand(deliveryId, signature, eventType, payload);

            await _mediator.Send(command); 

            await Send.OkAsync(); 
        }
    }
}
