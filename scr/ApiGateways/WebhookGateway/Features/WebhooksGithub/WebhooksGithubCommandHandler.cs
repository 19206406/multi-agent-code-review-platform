using BuildingBlocks.CQRS;
using MediatR;

namespace WebhookGateway.Features.WebhooksGithub
{
    public class WebhooksGithubCommandHandler : ICommandHandler<WebhooksGitHubCommand, WebhooksGithubResponse>
    {
        public WebhooksGithubCommandHandler()
        {
            
        }

        public async Task<WebhooksGithubResponse> Handle(WebhooksGitHubCommand command, CancellationToken cancellationToken)
        {
            if (command.EventType == "ping")
                return new WebhooksGithubResponse(true);

            if (command.EventType != "pull_request")
                return new WebhooksGithubResponse(true); 

            // 


            throw new NotImplementedException();
        }
    }
}
