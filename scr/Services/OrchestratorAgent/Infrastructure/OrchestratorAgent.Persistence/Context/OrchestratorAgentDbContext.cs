using Microsoft.EntityFrameworkCore;
using OrchestratorAgent.Domain.Entities;

namespace OrchestratorAgent.Persistence.Context
{
    public class OrchestratorAgentDbContext : DbContext
    {
        public OrchestratorAgentDbContext(DbContextOptions<OrchestratorAgentDbContext> options) : base(options)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrchestratorAgentDbContext).Assembly);
            base.OnModelCreating(modelBuilder); 
        }

        public DbSet<AgentTaskState> AgentTaskStates { get; set; }
        public DbSet<PipelineRun> PipelineRuns { get; set; }
    }
}
