using FastEndpoints;
using MediatR;

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
            Get("webhooks/github");
            AllowAnonymous(); 
        }

        public override async Task HandleAsync(CancellationToken ct)
        {

            var deliveryId = HttpContext.Request.Headers["X-GitHub-Delivery"].ToString();
            var signature = HttpContext.Request.Headers["X-Hub-Signature-256"].ToString();
            var eventType = HttpContext.Request.Headers["X-GitHub-Event"].ToString();

            // webhook body 
            var payload = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync(ct);

            var command = new WebhooksGitHubCommand(deliveryId, signature, eventType, payload);

            await _mediator.Send(command); 

            await Send.OkAsync(); 
        }
    }
}
