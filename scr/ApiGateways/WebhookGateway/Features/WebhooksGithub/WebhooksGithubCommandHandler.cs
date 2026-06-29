using BuildingBlocks.CQRS;
using MediatR;

namespace WebhookGateway.Features.WebhooksGithub
{
    public class WebhooksGithubCommandHandler : ICommandHandler<WebhooksGitHubCommand>
    {
        public WebhooksGithubCommandHandler()
        {
            
        }

        public Task<Unit> Handle(WebhooksGitHubCommand command, CancellationToken cancellationToken)
        {


            throw new NotImplementedException();
        }
    }
}
