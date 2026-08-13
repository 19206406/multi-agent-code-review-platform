using BuildingBlocks.CQRS;
using OrchestratorAgent.Application.Contracts.Persistence;
using OrchestratorAgent.Domain.Entities;

namespace OrchestratorAgent.Application.Features.Commands.CreatePipelineRun
{
    public class CreatePipelineRunCommandHandler : ICommandHandler<CreatePipelineRunCommand, CreatePipelineRunResponse>
    {
        private readonly IPipelineRunRepository _pipelineRunRepository;

        public CreatePipelineRunCommandHandler(IPipelineRunRepository pipelineRunRepository)
        {
            _pipelineRunRepository = pipelineRunRepository;
        }

        public async Task<CreatePipelineRunResponse> Handle(CreatePipelineRunCommand command, CancellationToken cancellationToken)
        {
            // TODO: Validatios for create PipelineRun 

            var newPipelineRun = new PipelineRun
            {
                CorrelationId = Guid.Parse(command.CorrelationId), 
                Status = 0, 
                RepositoryFullName = command.RepositoryName,
                PrNumber = command.PrNumber,
                PrTitle = command.PrTitle,
                PrAuthor = command.PrAuthor,
                HeadSha = command.HeadSha,
                BaseSha = command.BaseSha,
                HeadBranch = command.HeadBranch,
                BaseBranch = command.BaseBranch,
                GithubDeliveryId = command.GithubDeliveryId,
                RetryCount = 0,
                StartedAt = DateTimeOffset.UtcNow,
            };

            await _pipelineRunRepository.CratePipelineRunAsync(newPipelineRun);
            
            return new CreatePipelineRunResponse(newPipelineRun.Id);
        }
    }
}
