using BuildingBlocks.CQRS;

namespace OrchestratorAgent.Application.Features.Commands.CreatePipelineRun
{
    public record CreatePipelineRunCommand(
        string CorrelationId, 
        string RepositoryName, 
        int PrNumber,
        string PrTitle,
        string PrAuthor,
        string HeadSha, 
        string BaseSha,
        string HeadBranch,
        string BaseBranch,
        string GithubDeliveryId
        ) : ICommand<CreatePipelineRunResponse>; 
}
