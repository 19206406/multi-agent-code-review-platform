using OrchestratorAgent.Domain.Entities;

namespace OrchestratorAgent.Application.Contracts.Persistence
{
    public interface IPipelineRunRepository
    {
        Task CratePipelineRunAsync(PipelineRun pipeline); 
    }
}
