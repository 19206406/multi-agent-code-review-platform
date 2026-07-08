using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrchestratorAgent.Domain.Entities;

namespace OrchestratorAgent.Persistence.Configurations.Orchestrator
{
    public class AgentTaskStateConfiguration : IEntityTypeConfiguration<AgentTaskState>
    {
        public void Configure(EntityTypeBuilder<AgentTaskState> builder)
        {
            // table 
            builder.ToTable("agent_task_states", schema: "orchestrator");

            // Properties 
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasColumnName("id")
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(a => a.PipelineRunId)
                .HasColumnName("pipeline_run_id")
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(a => a.AgentType)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnType("agent_type");

            builder.Property(a => a.Status)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("status");

            // date by default—it's better to do it manually 
            builder.Property(a => a.DispatchedAt)
                .IsRequired()
                .HasColumnName("dispatched_at");

            builder.Property(a => a.CompletedAt)
                .HasColumnName("completed_at");

            builder.Property(a => a.RetryCount)
                .IsRequired()
                .HasDefaultValue(0)
                .HasColumnName("retry_count");

            builder.Property(a => a.LastError)
                .HasColumnType("text")
                .HasColumnName("last_error");

            builder.Property(a => a.ValidationPassed)
                .HasColumnName("validation_passed"); 

            builder.Property(a => a.ValidationReason)
                .HasColumnType("text")
                .HasColumnName("validation_reason");

            // indexes  
            builder.HasIndex(a => a.PipelineRunId)
                .HasDatabaseName("idx_agent_tasks_pipeline");
            builder.HasIndex(a => new { a.PipelineRunId, a.AgentType })
                .HasDatabaseName("idx_agent_tasks_unique"); 
        }
    }
}
