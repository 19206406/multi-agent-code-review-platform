using Microsoft.EntityFrameworkCore;
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
            await _context.AddAsync(pipeline);
            await _context.SaveChangesAsync();
        }

        public Task<PipelineRun?> PipelineRunByIdAsync(Guid id)
        {
            var pipelineRun = 
                _context.PipelineRuns.FirstOrDefaultAsync(p => p.Id == id);
            
            return pipelineRun;
        }
    }
}
