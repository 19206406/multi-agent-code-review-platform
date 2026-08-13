using BuildingBlocks.CQRS;

namespace OrchestratorAgent.Application.Features.Commands.EnrichPipelineRun
{
    public record EnrichPipelineRunCommand() : ICommand<EnrichPipelineRunResponse>;
}

