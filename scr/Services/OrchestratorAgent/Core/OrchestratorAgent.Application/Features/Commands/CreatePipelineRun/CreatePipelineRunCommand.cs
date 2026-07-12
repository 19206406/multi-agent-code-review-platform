using BuildingBlocks.CQRS;

namespace OrchestratorAgent.Application.Features.Commands.CreatePipelineRun
{
    public record CreatePipelineRunCommand() : ICommand<CreatePipelineRunResponse>; 
}
