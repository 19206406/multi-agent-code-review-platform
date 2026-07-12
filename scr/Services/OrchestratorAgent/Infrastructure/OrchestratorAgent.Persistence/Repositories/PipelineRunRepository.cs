using OrchestratorAgent.Application.Contracts.Persistence;
using OrchestratorAgent.Domain.Entities;

namespace OrchestratorAgent.Persistence.Repositories
{
    public class PipelineRunRepository : IPipelineRunRepository
    {
        public Task CratePipelineRunAsync(PipelineRun pipeline)
        {
            throw new NotImplementedException();
        }
    }
}
