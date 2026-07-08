using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrchestratorAgent.Domain.Entities;

namespace OrchestratorAgent.Persistence.Configurations.Orchestrator
{
    public class PipelineRunConfiguration : IEntityTypeConfiguration<PipelineRun>
    {
        public void Configure(EntityTypeBuilder<PipelineRun> builder)
        {
            // table 
            builder.ToTable("pipeline_runs", schema: "orchestrator");

            // Properties 
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .IsRequired()
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(e => e.CorrelationId)
                .IsRequired()
                .HasColumnType("uuid")
                .HasColumnName("correlation_id");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("status");

            builder.Property(e => e.RepositoryFullName)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("repository_full_name");

            builder.Property(e => e.PrNumber)
                .IsRequired()
                .HasColumnName("pr_number");

            builder.Property(e => e.PrTitle)
                .HasMaxLength(500)
                .HasColumnName("pr_title");

            builder.Property(e => e.PrAuthor)
                .HasMaxLength(150)
                .HasColumnName("pr_author");

            builder.Property(e => e.HeadSha)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("head_sha");

            builder.Property(e => e.BaseSha)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("base_sha");

            builder.Property(e => e.HeadBranch)
                .HasMaxLength(255)
                .HasColumnName("head_branch");

            builder.Property(e => e.BaseBranch)
                .HasMaxLength(255)
                .HasColumnName("base_branch");

            builder.HasIndex(e => e.GithubDeliveryId)
                .IsUnique(); 

            builder.Property(e => e.GithubDeliveryId)
                .HasMaxLength(250)
                .HasColumnName("github_delivery_id");

            builder.Property(e => e.RetryCount)
                .IsRequired()
                .HasDefaultValue(0)
                .HasColumnName("retry_count");

            builder.Property(e => e.ErrorMessage)
                .HasColumnType("text")
                .HasColumnName("error_message");

            builder.Property(e => e.EnrichedDiff)
                .HasColumnType("jsonb")
                .HasColumnName("enriched_diff");

            builder.Property(e => e.ChangedFiles)
                .HasColumnType("jsonb")
                .HasColumnName("changed_files");

            builder.Property(e => e.RagContext)
                .HasColumnType("jsonb")
                .HasColumnName("rag_context");

            // default value 
            builder.Property(e => e.StartedAt)
                .IsRequired()
                .HasColumnName("started_at");

            builder.Property(e => e.EnrichedAt)
                .HasColumnName("enriched_at");

            builder.Property(e => e.AgentsDispatchedAt)
                .HasColumnName("agents_dispatched_at");

            builder.Property(e => e.CompletedAt)
                .HasColumnName("complete_at");

            builder.Property(e => e.FailedAt)
                .HasColumnName("failed_at");

            builder.Property(e => e.GithubCommentId)
                .HasColumnType("bigint")
                .HasColumnName("github_comment_id");

            builder.Property(e => e.GithubCommentUrl)
                .HasMaxLength(500)
                .HasColumnName("github_comment_url");


            // indexes 
            builder.HasIndex(e => e.CorrelationId)
                .HasDatabaseName("idx_pipeline_runs_correlation_id");

            builder.HasIndex(e => e.RepositoryFullName)
                .HasDatabaseName("idx_pipeline_runs_repository");

            builder.HasIndex(e => e.Status)
                .HasDatabaseName("idx_pipeline_runs_status");

            builder.HasIndex(e => e.StartedAt)
                .HasDatabaseName("idx_pipeline_runs_started_at")
                .IsDescending(true); 
        }
    }
}
