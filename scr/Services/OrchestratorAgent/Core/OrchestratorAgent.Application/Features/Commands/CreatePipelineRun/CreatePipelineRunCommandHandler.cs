using BuildingBlocks.CQRS;
using OrchestratorAgent.Application.Contracts.Persistence;

namespace OrchestratorAgent.Application.Features.Commands.CreatePipelineRun
{
    public class CreatePipelineRunCommandHandler : ICommandHandler<CreatePipelineRunCommand, CreatePipelineRunResponse>
    {
        private readonly IPipelineRunRepository _pipelineRunRepository;

        public CreatePipelineRunCommandHandler(IPipelineRunRepository pipelineRunRepository)
        {
            _pipelineRunRepository = pipelineRunRepository;
        }

        public Task<CreatePipelineRunResponse> Handle(CreatePipelineRunCommand command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
