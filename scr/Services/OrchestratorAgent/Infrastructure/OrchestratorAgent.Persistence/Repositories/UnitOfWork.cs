using OrchestratorAgent.Application.Contracts.Persistence;
using OrchestratorAgent.Persistence.Context;

namespace OrchestratorAgent.Persistence.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly OrchestratorAgentDbContext _context;

        public UnitOfWork(OrchestratorAgentDbContext context)
        {
            _context = context;
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync(); 
        }
    }
}
