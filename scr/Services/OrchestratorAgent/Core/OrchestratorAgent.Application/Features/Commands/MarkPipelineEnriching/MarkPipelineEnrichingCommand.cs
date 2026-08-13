using BuildingBlocks.CQRS;

namespace OrchestratorAgent.Application.Features.Commands.MarkPipelineEnriching;

public record MarkPipelineEnrichingCommand(Guid PipelineId) : ICommand;