using BuildingBlocks.CQRS;

namespace OrchestratorAgent.Application.Features.Commands.EnrichPipelineRun
{
    public class EnrichPipelineRunCommandHandler : ICommandHandler<EnrichPipelineRunCommand, EnrichPipelineRunResponse>
    {
        public EnrichPipelineRunCommandHandler()
        {
            
        }
        
        public Task<EnrichPipelineRunResponse> Handle(EnrichPipelineRunCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

