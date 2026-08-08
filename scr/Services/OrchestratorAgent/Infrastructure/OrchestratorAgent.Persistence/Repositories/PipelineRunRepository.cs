using OrchestratorAgent.Application.Contracts.Persistence;
using OrchestratorAgent.Domain.Entities;
using OrchestratorAgent.Persistence.Context;

namespace OrchestratorAgent.Persistence.Repositories
{
    public class PipelineRunRepository : IPipelineRunRepository
    {
        private readonly OrchestratorAgentDbContext _context;

        public PipelineRunRepository(OrchestratorAgentDbContext context)
        {
            _context = context;
        }

        public async Task CratePipelineRunAsync(PipelineRun pipeline)
        {
            _context.Add(pipeline);
            await _context.SaveChangesAsync(); 
        }
    }
}
