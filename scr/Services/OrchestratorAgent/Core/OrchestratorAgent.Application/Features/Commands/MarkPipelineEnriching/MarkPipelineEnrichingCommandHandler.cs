using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using MediatR;
using OrchestratorAgent.Application.Contracts.Persistence;
using OrchestratorAgent.Domain.Entities;

namespace OrchestratorAgent.Application.Features.Commands.MarkPipelineEnriching;

public class MarkPipelineEnrichingCommandHandler : ICommandHandler<MarkPipelineEnrichingCommand>
{
    private readonly IPipelineRunRepository _pipelineRunRepository;
    private readonly IUnitOfWork _unitOfWork;

    public MarkPipelineEnrichingCommandHandler(IPipelineRunRepository pipelineRunRepository, IUnitOfWork unitOfWork)
    {
        _pipelineRunRepository = pipelineRunRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Unit> Handle(MarkPipelineEnrichingCommand command, CancellationToken cancellationToken)
    {
        var pipelineRun = await _pipelineRunRepository.PipelineRunByIdAsync(command.PipelineId);
        
        if (pipelineRun is null)
            throw new NotFoundException("PipelineRun", command.PipelineId);

        pipelineRun.Status = StatusPipelineRun.Enriching;

        await _unitOfWork.SaveChangesAsync();
        
        return Unit.Value;
    }
}